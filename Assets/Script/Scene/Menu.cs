using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void OnQuitButton()
    { 
        Application.Quit();
        EditorApplication.isPlaying= false;
    }
}
