using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPartType { LeftArm, RightArm, LeftFoot, RightFoot}

public class CharacterCtrl : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private float speed;

    [SerializeField]
    private KeyCode leftKey = KeyCode.A;

    [SerializeField]
    private KeyCode rightKey = KeyCode.D;

    [SerializeField]
    private KeyCode attackKey = KeyCode.Space;

    private int LeftArmParts = 3;
    private int RightArmParts = 3;

    [SerializeField]
    private Renderer torsoRenderer;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //renderers = gameObject.GetComponentsInChildren<Renderer>();
     }

    private void FixedUpdate()
    {
        CheckInputKeys();
        ScreenWrap();
    }

    private void CheckInputKeys()
    {
        if (GameStateManager.CurrentGameState != 1)
        {
            animator.SetBool("Walking", false);
            return;
        }

        if (Input.GetKeyDown(attackKey) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            animator.SetTrigger("Attacking");
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            if (Input.GetKey(leftKey))
            {
                animator.SetBool("Walking", true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Vector3.right * Time.deltaTime * speed);

            }
            else if (Input.GetKey(rightKey))
            {
                animator.SetBool("Walking", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
    }

    private void ScreenWrap()
    {
        if (torsoRenderer.isVisible)
        {
            return;
        }

        Vector3 newPosition = transform.position;
        if (newPosition.x > 1 || newPosition.x <0)
        {
            newPosition.x = -newPosition.x;
        }

        transform.position = newPosition;
    }

    public void NotifyCollision(BodyPartType type)
    {
        switch(type)
        {
            case BodyPartType.LeftArm: { LeftArmParts--;  break; }
            case BodyPartType.RightArm: { RightArmParts--; break; }
        }
        
        if (LeftArmParts<RightArmParts)
        {
            animator.SetBool("RightArmAttack", true);
        }
        else
        {
            animator.SetBool("RightArmAttack", false);
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            animator.SetTrigger("Hurt");
            
        }

        speed += 1.5f;

        if (LeftArmParts == 0 && RightArmParts == 0)
        {
            GameStateManager.CurrentGameState = 3;
        }
    }
}
