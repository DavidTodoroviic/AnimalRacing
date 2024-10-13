using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject foxPrefab;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        
        GameObject bear = GameObject.Find("BlackBear");
        GameObject gorilla = GameObject.Find("Gorilla_01");
        GameObject fox = null;

        if (selectedCharacter == 2)
        {
            if (foxPrefab != null)
            {
                fox = Instantiate(foxPrefab, transform.position, Quaternion.identity);
            }
        }

        switch (selectedCharacter)
        {
            case 0:
                if (bear != null)
                {
                    bear.GetComponent<AI>().isPlayerControlled = true;
                }
                break;

            case 1:
                if (gorilla != null)
                {
                    gorilla.GetComponent<AI>().isPlayerControlled = true;
                }
                break;

            case 2:
                if (fox != null)
                {
                    fox.GetComponent<AI>().isPlayerControlled = true;
                }
                break;

            default:
                break;
        }
    }
}
