using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseManager : MonoBehaviour
{
    void Awake()
    {
        // Optional: Keep this GameObject alive between scenes
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called whenever a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnableMouseCursor();
    }

    // Method to enable the mouse cursor
    private void EnableMouseCursor()
    {
        Cursor.visible = true;                 // Make the mouse cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }
}