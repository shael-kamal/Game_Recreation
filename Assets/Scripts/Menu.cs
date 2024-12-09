using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void OnPlayPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

}
