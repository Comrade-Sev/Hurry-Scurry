using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetting : MonoBehaviour
{
    public int diffLevel = 0;

    //public void levelChosen()
    //{
    //    PlayerPrefs.SetInt("difficultyLevel", diffLevel);
    //}
    
    public static GameObject sampleInstance;
    private void Awake()
    {
        if (sampleInstance != null)
            Destroy(sampleInstance);

        sampleInstance = gameObject;
        DontDestroyOnLoad(this);
    }

    public void level1()
    {
        diffLevel = 1;
    }
    public void level2()
    {
        diffLevel = 2;
    }
    public void level3()
    {
        diffLevel = 3;
    }
    public void level4()
    {
        diffLevel = 4;
    }
    public void level5()
    {
        diffLevel = 5;
    }

    //public void LoadNumber()
    //{
        //int loadedNumber = PlayerPrefs.GetInt(difficultyLevel);
    //}
}