using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LapCounterUI : MonoBehaviour
{
    public LapCounter lapCounter; // Reference to the LapCounter script
    public Text lapText; // UI Text element to display the lap count

    private void Update()
    {
        if (lapCounter != null && lapText != null)
        {
            lapText.text = "Lap " + lapCounter.GetCurrentLap() + "/" + lapCounter.totalLaps;
        }
    }
}
