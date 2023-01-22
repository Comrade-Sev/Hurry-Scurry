using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{

    public Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSceneToLoading()
    {
        SceneManager.LoadScene("Loading");
    }

    public void ChangeSceneToDeath()
    {
        SceneManager.LoadScene("HighScore");
    }
    public void ChangeSceneMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ChangeSceneSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    
}
