// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     public Transform player;         // Reference to the player (bear) object
//     public Vector3 offset = new Vector3(484.1315, -0.2000003, 931.3401);  // Camera offset from the player
//     public float smoothSpeed = 0.125f;  // Smooth speed for camera movement

//     void LateUpdate()
//     {
//         // Keep the camera at a fixed offset from the player
//         Vector3 desiredPosition = player.position + offset;
//         Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
//         transform.position = smoothedPosition;

//         // Keep the camera's rotation fixed, looking at the player, or you can set it manually
//         transform.LookAt(player);  // Optional if you want the camera to look at the player
//     }
// }
