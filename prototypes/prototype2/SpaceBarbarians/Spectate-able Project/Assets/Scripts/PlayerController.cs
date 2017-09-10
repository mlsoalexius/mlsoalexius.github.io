using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Player m_player;

    [Header("Controls:")]
    [SerializeField]
    private KeyCode m_moveLeft = KeyCode.A;

    [SerializeField]
    private KeyCode m_moveRight = KeyCode.D;

    [SerializeField]
    private KeyCode m_attack = KeyCode.Space;

    [SerializeField]
    private KeyCode m_jump = KeyCode.W;
    #endregion

    #region Properties

    public Player Player
    {
        get
        {
            return m_player;
        }
    }

    #endregion

    private void Awake()
    {
        if (!m_player)
        {
            m_player = GetComponent<Player>();
        }
        if (!m_player)
        {
            Debug.LogError("There isn't any Player attached to the controller!", this);
        }
    }

    private void Update()
    {
        if (GameStateManager.instance)
            if (GameStateManager.CurrentGameState != 1)
            {
                return;
            }

        if (Input.GetKeyDown(m_attack))
        {
            Player.Attack();
        }
        if (Input.GetKeyDown(m_jump))
        {
            Player.Jump();
        }
        if (Input.GetKey(m_moveLeft))
        {
            Player.Move(-1);
        }
        else if (Input.GetKey(m_moveRight))
        {
            Player.Move(1);
        }
        else
        {
            Player.Move(0);
        }
    }

}
