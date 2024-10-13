using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalSelectionPointTransition : MonoBehaviour
{
    public string sceneName = "AnimalSelectionScene"; // Replace with the actual name of your animal selection scene

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
