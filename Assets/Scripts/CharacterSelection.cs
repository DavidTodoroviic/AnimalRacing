using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Add this if using TextMeshPro

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters; // Array of character game objects
    public int selectedCharacter = 0;
    public TextMeshProUGUI characterNameText;  // Reference to the TextMeshPro UI element

    private Animator currentAnimator;

    void Start()
    {
        // Set the initial character active and update the text
        ActivateCharacter(selectedCharacter);
    }

    public void NextCharacter()
    {
        // Deactivate the current character
        DeactivateCharacter(selectedCharacter);
        
        // Update selectedCharacter index to next character
        selectedCharacter = (selectedCharacter + 1) % characters.Length;

        // Activate the new character and update the text
        ActivateCharacter(selectedCharacter);
    }

    public void PreviousCharacter()
    {
        // Deactivate the current character
        DeactivateCharacter(selectedCharacter);
        
        // Update selectedCharacter index to previous character
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }

        // Activate the new character and update the text
        ActivateCharacter(selectedCharacter);
    }

    public void StartGame()
    {
        // Store the selected character index and load the next scene
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void StartGameRace()
    {
        // Store the selected character index and load the next scene
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }

    public void MakeCharacterWave()
    {
        if (currentAnimator != null)
        {
            // Set the WaveTrigger to initiate the wave animation
            currentAnimator.SetTrigger("WaveTrigger");
        }
    }

    private void ActivateCharacter(int index)
    {
        // Activate the selected character in the scene
        characters[index].SetActive(true);

        // Get the Animator component of the newly activated character
        currentAnimator = characters[index].GetComponent<Animator>();

        // Update the character name text
        if (characterNameText != null)
        {
            characterNameText.text = characters[index].name; // Assuming the GameObject name is the character name
        }
    }

    private void DeactivateCharacter(int index)
    {
        // Deactivate the character in the scene
        characters[index].SetActive(false);
    }
}
