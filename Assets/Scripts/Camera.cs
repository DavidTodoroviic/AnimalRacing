using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float distance = 5.0f;
    public float height = 3.0f;
    public float damping = 2.0f;

    private Transform currentPlayer;
    private Vector3 targetPosition;

    void LateUpdate()
    {
        UpdateCurrentPlayer();

        if (currentPlayer != null)
        {
            targetPosition = currentPlayer.position - currentPlayer.forward * distance;
            targetPosition.y = currentPlayer.position.y + height;

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);
            transform.LookAt(currentPlayer.position);
        }
    }

    void UpdateCurrentPlayer()
    {
        currentPlayer = null;
        AI[] players = FindObjectsOfType<AI>(); // Find all AI components in the scene

        foreach (AI playerAI in players)
        {
            if (playerAI.isPlayerControlled)
            {
                currentPlayer = playerAI.transform; // Get the transform of the controlled player
                break; // Exit loop after finding the controlled player
            }
        }
    }
}
