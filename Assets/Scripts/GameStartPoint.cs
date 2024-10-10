using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartPointTransition : MonoBehaviour
{
    public string sceneName = "GameStartScene";

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
