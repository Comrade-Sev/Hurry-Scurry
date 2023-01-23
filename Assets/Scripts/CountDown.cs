using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{


    public int countDownTime;
    public Text countDownDisplay;

    IEnumerator CountDownToStart()
    {
        while(countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

        countDownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countDownDisplay.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
