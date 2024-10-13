using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
       public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}