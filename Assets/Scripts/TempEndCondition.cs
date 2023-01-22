using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using LootLocker.Requests;

public class TempEndCondition : MonoBehaviour
{
    private int score = 0;
    private bool Kill = false;
    public Leaderboard leaderboard;
    private string memberID = "Player1";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("F"))
        // {
        //     Kill = true;
        // }

        if (Input.GetKeyDown("P"))
        {
            score++;
        }
    }

    // IEnumerator DieRoutine()
    // {
    //     if (Kill = true)
    //     {
    //         Time.timeScale = 0f;
    //         yield return new WaitForSecondsRealtime(1f);
    //         yield return leaderboard.SubmitScoreRoutine(score);
    //         Time.timeScale = 1f;
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //     }
    // }
}
