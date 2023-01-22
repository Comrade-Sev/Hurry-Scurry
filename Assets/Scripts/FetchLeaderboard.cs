using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;


public class FetchLeaderboard : MonoBehaviour
{
    public int leaderboardID = 10607;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    public int count;

    private void Start()
    {
        
        MenuProccess();
        
    }

    void MenuProccess()
    {
        StartCoroutine(LoginRoutine());
        StartCoroutine(FetchTopHighscoresRoutine());
        StartCoroutine(LogoutRoutine());
    }
    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderboardID, count, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if(members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }

                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile((() => done == false));
    }
    
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
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
    
    IEnumerator LogoutRoutine()
    {
        bool done = false;
        LootLockerSDKManager.EndSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged out");
                done = true;
            }
            else
            {
                Debug.Log("Could not end session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
