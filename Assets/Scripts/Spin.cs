using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{

   

    public int flag;

    

    public void Rotate () {
        flag = 1;
        //Debug.Log(flag);
    }

    public void Reset () {
        flag = 0;
        Debug.Log(flag);
    }
   
}