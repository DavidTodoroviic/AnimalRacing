using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes

public class LapCounter : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public int totalLaps = 3; 
    private int currentLap = 1; 
    private bool hasFinishedLap = false;

    // Public variable to specify the name of the results scene
    public string resultsSceneName = "ResultsScene"; // Make sure this matches the actual scene name in Unity

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player object
        if (other.gameObject == player)
        {
            if (!hasFinishedLap)
            {
                hasFinishedLap = true;
                currentLap++;

                if (currentLap > totalLaps)
                {
                    currentLap = totalLaps; // Prevent exceeding total laps
                    EndRace(); // Call method to handle the end of the race
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider is the player object
        if (other.gameObject == player)
        {
            hasFinishedLap = false;
        }
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }

    // Handle the end of the race
    private void EndRace()
    {
        // Optionally save race data (e.g., lap count) to be displayed in the results scene
        PlayerPrefs.SetInt("FinalLapCount", totalLaps);

        // Load the results scene using the correct scene name
        SceneManager.LoadScene(resultsSceneName);
    }
}