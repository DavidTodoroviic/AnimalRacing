using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AI : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10f;
    public float turnSpeed = 2f;
    public float waypointRadius = 3f;
 
    private int currentWaypointIndex = 0;
    public bool isPlayerControlled = false;
 
    public Transform cameraTransform;
    public string terrainTag = "Terrain";
 
    void Update()
    {
        if (isPlayerControlled)
        {
            HandlePlayerInput();
        }
        else
        {
            NavigateWaypoints();
        }
 
        if (IsOffTrack())
        {
            TeleportToNearestWaypoint();
        }
    }
 
    void NavigateWaypoints()
    {
        if (waypoints.Length == 0) return;
 
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
 
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
 
        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointRadius)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
 
    void HandlePlayerInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
 
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
 
        forward.y = 0f;
        right.y = 0f;
 
        forward.Normalize();
        right.Normalize();
 
        Vector3 movement = forward * moveVertical + right * moveHorizontal;
        transform.position += movement * speed * Time.deltaTime;
 
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
 
    bool IsOffTrack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag(terrainTag))
            {
                return true;
            }
        }
        return false;
    }
 
    void TeleportToNearestWaypoint()
    {
        float closestDistance = Mathf.Infinity;
        Transform nearestWaypoint = null;
 
        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, waypoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestWaypoint = waypoints[i];
            }
        }
 
        if (nearestWaypoint != null)
        {
            transform.position = nearestWaypoint.position;
        }
    }
}
 