using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        
    }
    public static void LoadScene()
    {

        SceneManager.LoadScene("GameMode");
    }
    public static void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public static void Settings()
    { 
        
    
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("menu");
    }


}
