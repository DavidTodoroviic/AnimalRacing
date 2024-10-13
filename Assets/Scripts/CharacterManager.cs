using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

        // Find the character GameObjects in the scene
        GameObject bear = GameObject.Find("BlackBear");
        GameObject gorilla = GameObject.Find("Gorilla_01");
        GameObject fox = GameObject.Find("fox");

        // Set isPlayerControlled based on selectedCharacter
        if (bear != null && selectedCharacter == 0) // The index for Bear
        {
            bear.GetComponent<AI>().isPlayerControlled = true;
        }
        else if (gorilla != null && selectedCharacter == 1) // The index for Gorilla
        {
            gorilla.GetComponent<AI>().isPlayerControlled = true;
        }
        else if (fox != null && selectedCharacter == 2) // The index for Fox
        {
            fox.GetComponent<AI>().isPlayerControlled = true;
        }
    }
}
