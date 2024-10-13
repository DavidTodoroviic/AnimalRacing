using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartPointTransition : MonoBehaviour
{
    public string sceneName = "Desert";

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return))
        {
            // Turn the mouse cursor on and unlock it
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
}