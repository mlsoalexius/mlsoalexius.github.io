using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //public float TotalTimeLeft = 120f;
    public Text TimerText, RoundTimerText, Player1Points, Player2Points, TutorialText;
    public Image Player1WinImage, Player2WinImage;
    private int seconds = 0, minutes = 0;
    private bool paused = false;


    // Use this for initialization
    void Start()
    {
        Player1WinImage.enabled = false;
        Player2WinImage.enabled = false;

        GameStateManager.RegisterUI(this);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStateManager.CurrentGameState)
        {
            case 0:
                Player1WinImage.enabled = false;
                Player2WinImage.enabled = false;
                TimerText.enabled = false;
                RoundTimerText.enabled = false;
                Player1Points.enabled = false;
                Player2Points.enabled = false;
                //NEW GAME
                if (!TutorialText.enabled)
                    TutorialText.enabled = true;
               
                break;
            case 1:
                //PLAYING
                TimerText.enabled = true;
                Player1Points.enabled = true;
                Player2Points.enabled = true;
                TutorialText.enabled = false;
                TimerText.enabled = true;
                RoundTimerText.enabled = false;
                UpdateTimer();
                break;
            case 2:
                //PAUSED
                if (!TutorialText.enabled)
                    TutorialText.enabled = true;
                break;
            case 3:
                //GAME OVER
                //Player win logic
                if (GameStateManager.player1Score > GameStateManager.player2Score)
                    Player1WinImage.enabled = true;
                else
                    Player2WinImage.enabled = true;
                break;
            case 4:
                TimerText.enabled = false;
                RoundTimerText.enabled = true;
                UpdateRoundTimer();
                break;
        }
    }
    /// <summary>
    /// This function is used to one declare the winner of the previous round,
    /// it updates the points shown on screen,
    /// and resets the round
    /// </summary>
    public void UpdateRoundGUI()
    {
            Player1Points.text = GameStateManager.player1Score.ToString();
       
            Player2Points.text = GameStateManager.player2Score.ToString();
        
    }

    private void UpdateTimer()
    {
        seconds = (int)GameStateManager.TimeLeft % 60;
        minutes = ((int)GameStateManager.TimeLeft / 60) % 60;
        if (seconds < 10)
        {
            TimerText.text = "0" + minutes.ToString() + " : 0" + seconds;
        }
        else
            TimerText.text = "0" + minutes.ToString() + " : " + seconds; //TimeLeft.ToString();
    }
    private void UpdateRoundTimer()
    {
        seconds = (int)GameStateManager.TimeLeft % 60;
        minutes = ((int)GameStateManager.TimeLeft / 60) % 60;
        if (seconds < 10)
        {
            RoundTimerText.text = "0" + minutes.ToString() + " : 0" + seconds;
        }
        else
            RoundTimerText.text = "0" + minutes.ToString() + " : " + seconds; //TimeLeft.ToString();
    }
}
