using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerNumber{Player1, Player2}

public class Player : MonoBehaviour
{
    #region Fields

    private ScreenWrapper m_wrapper;

    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private float m_speed = 20f;

    [SerializeField]
    private int m_jumpForce = 1000;

    [SerializeField]
    private int m_maxJumps = 2;

    [SerializeField]
    private int m_jumpsLeft;

    [SerializeField]
    private float m_attackTime = 1;

    [SerializeField]
    private bool m_isAlive = true;

    //private float m_timeUntilAttack = 0;

    private bool m_isJumping;

    private Rigidbody2D m_rb;

    [SerializeField]
    private Renderer torsoRenderer;

    public int LeftArmParts = 3;
    public int RightArmParts = 3;

    public PlayerNumber playerNumber;

    private AudioSource audioSource;

    private Attacking m_animationBehaviour;
    #endregion

    #region Properties
    public Animator Animator
    {
        get
        {
            return this.m_animator;
        }

        set
        {
            this.m_animator = value;
        }
    }

    public float Speed
    {
        get
        {
            return this.m_speed;
        }

        set
        {
            this.m_speed = value;
        }
    }

    public int JumpForce
    {
        get
        {
            return this.m_jumpForce;
        }

        set
        {
            this.m_jumpForce = value;
        }
    }

    public int MaxJumps
    {
        get
        {
            return this.m_maxJumps;
        }

        set
        {
            this.m_maxJumps = value;
        }
    }

    public int JumpsLeft
    {
        get
        {
            return this.m_jumpsLeft;
        }

        set
        {
            this.m_jumpsLeft = value;
        }
    }

    public float AttackTime
    {
        get
        {
            return this.m_attackTime;
        }

        set
        {
            this.m_attackTime = value;
        }
    }

    public bool CanAttack
    {
        get
        {
            return !m_animationBehaviour.IsAttacking;
          
            /* if (m_timeUntilAttack >= 0)
             {
                 return false;
             }
             return true;*/
        }
    }

    public bool IsAlive
    {
        get
        {
            return m_isAlive;
        }
        set
        {
            this.m_isAlive = value;
        }
    }

    public bool IsJumping
    {
        get
        {
            return m_isJumping;
        }
    }

    #endregion

    private void Awake()
    {
        JumpsLeft = MaxJumps;

        if (!m_rb)
        {
            m_rb = GetComponent<Rigidbody2D>();
        }
        if (!m_rb)
        {
            Debug.LogError("No Rigidbody2D attached!", this);
        }
        if (!Animator)
        {
            Animator = GetComponent<Animator>();
        }
        if (!Animator)
        {
            Debug.LogError("There isn't any Animator attached to the controller!", this);
        }

        if(!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }

        GameStateManager.RegisterPlayer(playerNumber, this);
        m_animationBehaviour = m_animator.GetBehaviour<Attacking>();
        m_wrapper = new ScreenWrapper();
    }

    private void Update()
    {
   /*     if (!CanAttack)
        {
            m_timeUntilAttack -= Time.deltaTime;
        } */
        if(IsJumping && CanAttack)
        {
            Animator.SetBool("Jumping", true);
        }
        else
        {
            Animator.SetBool("Jumping", false);
        }

        if(GameStateManager.CurrentGameState == 1 && GameStateManager.IsSceneLoaded)
        {
             ScreenWrap();
        }   
    }

    public void Jump()
    {
        if (JumpsLeft > 0)
        {
            PlayAudioClip(GameStateManager.instance.GetGruntShort);
            m_rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Force);
            --JumpsLeft;
        }
    }

    public void Move(float direction)
    {
        if (direction != 0)
        {
            Rotate(direction);
            if (!IsJumping)
            {
                Animator.SetBool("Walking", true);
            }
            else
            {
                Animator.SetBool("Walking", false);
            }
        }
        else
        {
            Stop();
        }
        if (CanAttack)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime * Mathf.Abs(direction));
        }
        else
        {
            Animator.SetBool("Walking", false);
        }
    }

    public void Rotate(float direction)
    {
        if (direction < 0)
        {
            // Rotate Left
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            // Rotate Right
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Stop()
    {
        Animator.SetBool("Walking", false);
    }

    public void Attack()
    {
        if (CanAttack)
        {
            PlayAudioClip(GameStateManager.instance.GetHitWhoosh);
            Animator.SetTrigger("Attacking");
         //   m_timeUntilAttack = m_attackTime;
        }
    }
    
    private void ScreenWrap()
    {
        if (!torsoRenderer.isVisible && torsoRenderer.enabled)
        {
           gameObject.transform.position  = m_wrapper.Wrap(gameObject.transform.position);
            //Vector3 newPosition = transform.position;
            //if (newPosition.x > 1 || newPosition.x < 0)
            //{
            //    newPosition.x = -newPosition.x;
            //}

            //transform.position = newPosition;
        }
    }

    public void NotifyCollision(BodyPartType type)
    {
        switch (type)
        {
            case BodyPartType.LeftArm: { LeftArmParts--; break; }
            case BodyPartType.RightArm: { RightArmParts--; break; }
        }

        if (LeftArmParts < RightArmParts)
        {
            Animator.SetBool("RightArmAttack", true);
        }
        else
        {
            Animator.SetBool("RightArmAttack", false);
        }
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            Animator.SetTrigger("Hurt");

        }

        m_speed += 1.5f;
        JumpForce += 100;

        PlayAudioClip(GameStateManager.instance.GetGrunt);

        if (LeftArmParts <= 0 && RightArmParts <= 0)
        {
            GameStateManager.LostRound(playerNumber);
        }
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" && collision.gameObject.transform.position.y < transform.position.y)
        {
            JumpsLeft = MaxJumps;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            m_isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_isJumping = true;
    }

    private void PlayAudioClip(AudioClip clip)
    {
        audioSource.pitch = 1 + Random.Range(-0.15f, 0.15f);
        audioSource.clip = clip;
        audioSource.Play();
    }
}
