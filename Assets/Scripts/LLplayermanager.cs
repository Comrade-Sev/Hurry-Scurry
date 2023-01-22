using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using Random = UnityEngine.Random;

public class LLplayermanager : MonoBehaviour
{
    public Leaderboard leaderboard;

    public TMP_InputField playerNameInputfield;

    private float playerIDFloat;
    public int playerID;
    //private float oldPlayerId;
    
    // Start is called before the first frame update
    void Start()
    {
        generatePlayerID();
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
        {
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
}
