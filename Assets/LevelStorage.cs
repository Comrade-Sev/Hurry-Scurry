using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStorage : MonoBehaviour
{
    public DifficultySetting number;
    public int num1;

    // Update is called once per frame
    void Update()
    {
        num1 = number.diffLevel;
    }
}
