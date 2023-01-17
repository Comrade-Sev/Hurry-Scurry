using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class Leaderboard : MonoBehaviour
{
    private int LeaderboardID = 10607;

    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator SubmitScoreRoutine(int score)
    {
        bool done = false;
        string memberID = PlayerPrefs.GetString("MemberID");
        LootLockerSDKManager.SubmitScore(memberID, score, LeaderboardID, (response =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        }));
        yield return new WaitWhile((() => done == false));
    }
}
