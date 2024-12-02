using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnReplayPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
    
}
