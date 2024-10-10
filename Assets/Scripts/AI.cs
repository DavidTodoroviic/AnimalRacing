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

    private bool isSpinning = false;

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
        if (isSpinning) return;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BananaPeel"))
        {
            Destroy(other.gameObject);
            StartCoroutine(SpinPlayer(1.0f));
        }
    }

    private IEnumerator SpinPlayer(float spinDuration)
    {
        isSpinning = true;

        float startRotation = transform.eulerAngles.y;
        float targetRotation = startRotation + 360f;
        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            float newYRotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime / spinDuration);
            transform.eulerAngles = new Vector3(0, newYRotation, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = new Vector3(0, targetRotation, 0);
        isSpinning = false;
    }
}
