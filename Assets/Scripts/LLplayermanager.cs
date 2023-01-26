using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using RunRun3;
using TMPro;
using Random = UnityEngine.Random;

public class LLplayermanager : MonoBehaviour
{
    private UnityEngine.TouchScreenKeyboard keyboard;
    public static string keyboardText = "";
    
    public Leaderboard leaderboard;

    public TMP_InputField playerNameInputfield;

    private float playerIDFloat;
    public int playerID;
    public int score;

    //public Player score;
    //private float oldPlayerId;
    
    // Start is called before the first frame updates
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        generatePlayerID();
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        TouchScreenKeyboard.Open("Enter Email:", TouchScreenKeyboardType.EmailAddress, false, false, false);
        keyboardText = keyboard.text;
        LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
        {
            keyboard.text = playerNameInputfield.text;
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name" + response.Error);
            }
        });
    }

    public void generatePlayerID()
    {
        playerIDFloat = Random.Range(0f, 10000f);
        playerID = Convert.ToInt32(playerIDFloat);
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
        StartCoroutine(DieRoutine());
    }
    
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession(playerID.ToString(),(response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                //PlayerPrefs.SetString("MemberID", response.player_id.ToString());
                //("MemberID", playerID.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    
    IEnumerator DieRoutine()
    {
        Time.timeScale = 0f;
        //scoreCalc();
        yield return new WaitForSecondsRealtime(1f);
        yield return leaderboard.SubmitScoreRoutine(score);
        Time.timeScale = 1f;
    }
}
