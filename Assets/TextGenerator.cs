using TMPro;  // Required to work with TextMeshPro
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
    // Reference to the TextMeshPro text object
    public TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        // Set initial text
        SetText("Hello, World! \u2705");
    }

    // Method to update text dynamically
    public void SetText(string newText)
    {
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = newText;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI reference is missing!");
        }
    }
}
