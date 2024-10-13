using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearRage : MonoBehaviour
{
    public float rageIncreaseRate = 0.2f;       // How fast the rage fills
    public float rageRadius = 10f;              // Radius for detecting other animals
    public float rageBoostDuration = 5f;        // Duration of the rage boost
    public float accelerationBoost = 5f;        // Boosted acceleration value
    public float sizeMultiplier = 1.5f;         // Bear size multiplier during rage

    private float rage = 0f;                    // Current rage level (0 to 1)
    public bool isRaging = false;              // Is the bear in rage mode

    public Slider rageBar;                      // UI Slider for Rage Bar
    public Transform bearTransform;             // Reference to the bear's transform

    public List<GameObject> targetAnimals;      // List of specific animals to track

    private Vector3 originalSize;               // Original size of the bear

    void Start()
    {
        originalSize = bearTransform.localScale;

        rageBar.value = rage;                    // Initialize rage bar
    }

    void Update()
    {
        if (!isRaging)
        {
            // Check if any of the target animals are within range and fill the rage bar
            foreach (GameObject animal in targetAnimals)
            {
                if (animal != null && Vector3.Distance(transform.position, animal.transform.position) <= rageRadius)
                {
                    rage += rageIncreaseRate * Time.deltaTime;
                    rage = Mathf.Clamp(rage, 0f, 1f); // Keep rage between 0 and 1

                    rageBar.value = rage;            // Update UI
                    break;  // Only need one animal to trigger rage increase
                }
            }

            // Check if rage is full and trigger rage mode
            if (rage >= 1f)
            {
                StartCoroutine(TriggerRageMode());
                
                // Find the closest animal within the radius and pause its position for 1 second
                foreach (GameObject animal in targetAnimals)
                    {
                        if (animal != null && Vector3.Distance(transform.position, animal.transform.position) <= rageRadius)
                    {
                         StartCoroutine(PauseAnimal(animal, 1f));  // Pause the animal's position for 1 second
                            break;
                         }
                     }
            }
        }
    }


    private IEnumerator PauseAnimal(GameObject animal, float duration)
    {
    // Store the current position
        Vector3 originalPosition = animal.transform.position;

    // Keep the animal at the same position for the duration
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            animal.transform.position = originalPosition;  // Keep the position fixed
            elapsedTime += Time.deltaTime;
         yield return null;
        }
    }

    IEnumerator TriggerRageMode()
    {
        isRaging = true;

        // Increase size and acceleration
        bearTransform.localScale = originalSize * sizeMultiplier;

        // Rage mode lasts for a limited time
        yield return new WaitForSeconds(rageBoostDuration);

        EndRageMode();
    }


    void EndRageMode()
    {
        isRaging = false;
        rage = 0f;  // Reset rage
        rageBar.value = rage;

        // Return bear to original size and reset acceleration
        bearTransform.localScale = originalSize;
    }

    // Optional: Draw the radius in the editor to visualize it
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rageRadius);
    }
}
