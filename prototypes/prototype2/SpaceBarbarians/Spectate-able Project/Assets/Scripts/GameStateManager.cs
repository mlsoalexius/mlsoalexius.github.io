using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static int CurrentGameState = 0;
    public GameObject Canvas;
    public static Player Player1, Player2;
    public float TotalTimeLeft = 120f;
    public static float TimeLeft = 120f;
    public static int player1Score, player2Score;

    private static bool isSceneLoaded = true;

    public static GameStateManager instance = null;

    #region AUDIO
    public List<AudioClip> grunt;

    public List<AudioClip> gruntShort;

    public List<AudioClip> hitWhoosh;

    public List<AudioClip> swordHit;

    public List<AudioClip> music;


    public AudioSource musicSource;
    #endregion

    public AudioClip GetGrunt
    {
        get
        {
            return grunt[Random.Range(0, grunt.Count)];
        }
    }

    public AudioClip GetGruntShort
    {
        get
        {
            return gruntShort[Random.Range(0, gruntShort.Count)];
        }
    }

    public AudioClip GetHitWhoosh
    {
        get
        {
            return hitWhoosh[Random.Range(0, hitWhoosh.Count)];
        }
    }

    public AudioClip GetMusicClip
    {
        get
        {
            return music[Random.Range(0, music.Count)];
        }
    }

    public static bool IsSceneLoaded
    {
        get
        {
            return isSceneLoaded;
        }
    }
  

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            CurrentGameState = (int)GameState.New;
        }
        if (!musicSource)
        {
            musicSource = GetComponent<AudioSource>();
        }

        music = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/Music"));
        grunt = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/Grunts"));
        gruntShort = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/Grunts_Short"));
        hitWhoosh = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/Hit_Whoosh"));
        hitWhoosh = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/Hit_Whoosh"));

        RandomizeMusic();
    }

    private static UI UIScript;
    private enum GameState
    {
        New,
        Playing,
        Paused,
        GameOver,
        RoundStart
    };

    // Use this for initialization
    void Start()
    {
        TimeLeft = TotalTimeLeft;
        UIScript = Canvas.GetComponent<UI>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentGameState)
        {
            case 0:
                //NEW GAME
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    InitNewGame();
                    CurrentGameState = (int)GameState.Playing;
                }
                break;
            case 1:

                //PLAYING
                TimeLeft -= Time.deltaTime;
                if (TimeLeft <= 0)
                {
                    if(Player1.LeftArmParts + Player1.RightArmParts == Player2.RightArmParts + Player2.LeftArmParts)
                    {
                        TimeLeft = 4f;
                        CurrentGameState = 4;
                    }
                    else if (Player1.LeftArmParts + Player1.RightArmParts > Player2.RightArmParts + Player2.LeftArmParts)
                    {
                        LostRound(PlayerNumber.Player2);
                    }
                    else
                    {
                        LostRound(PlayerNumber.Player1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (CurrentGameState == (int)GameState.Paused)
                        CurrentGameState = (int)GameState.Playing;
                    else
                        CurrentGameState = (int)GameState.Paused;
                }
                break;
            case 2:
                //PAUSED
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CurrentGameState = (int)GameState.Playing;
                }
                break;
            case 3:
                //GAME OVER
                //      
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CurrentGameState = (int)GameState.New;
                    isSceneLoaded = false;
                    SceneManager.LoadScene(0);
                    
                }
                //Player win logic
                break;
            case 4:
                TimeLeft -= Time.deltaTime;
                if (TimeLeft <= 0)
                {
                    CurrentGameState = (int)GameState.Playing;
                    TimeLeft = TotalTimeLeft;
                    isSceneLoaded = false;
                    SceneManager.LoadScene(0);
                }

                break;
        }
    }
    private void InitNewGame()
    {
        TimeLeft = TotalTimeLeft;
        player1Score = 0;
        player2Score = 0;
        UIScript.UpdateRoundGUI();

    }

    public static void LostRound(PlayerNumber playerNumber)
    {
        if (playerNumber == PlayerNumber.Player1)
        {
            player2Score++;
            UIScript.UpdateRoundGUI();
        }
        else
        {
            player1Score++;
            UIScript.UpdateRoundGUI();
        }

        if (player1Score >= 3 || player2Score >= 3)
        {
            CurrentGameState = 3;
        }
        else
        {
            TimeLeft = 4f;
            CurrentGameState = 4;
        }
    }

    public void RandomizeMusic()
    {
        musicSource.clip = GetMusicClip;
        musicSource.Play();
    }

    public static void RegisterUI(UI ui)
    {
        UIScript = ui;
        UIScript.UpdateRoundGUI();
    }

    public static void RegisterPlayer(PlayerNumber playerNumber, Player player)
    {
        if(playerNumber == PlayerNumber.Player1)
        {
            Player1 = player;
        }
        else
        {
            Player2 = player;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneLoaded = true;
    }
}
