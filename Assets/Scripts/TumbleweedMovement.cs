using UnityEngine;

public class TumbleweedMovement : MonoBehaviour
{
    public float windStrength = 5f;
    public float speed = 5f;
    public Vector3 windDirection = new Vector3(1, 0, 0);
    private Vector3 centerPoint = new Vector3(948, 0, 1071);
    private float clampDistance = 500f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(windDirection * windStrength, ForceMode.Force);
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, centerPoint.x - clampDistance, centerPoint.x + clampDistance),
            transform.position.y,
            Mathf.Clamp(transform.position.z, centerPoint.z - clampDistance, centerPoint.z + clampDistance)
        );
        transform.position = clampedPosition;
        if (transform.position.x >= centerPoint.x + clampDistance || transform.position.x <= centerPoint.x - clampDistance)
        {
            windDirection.x = -windDirection.x;
        }

        if (transform.position.z >= centerPoint.z + clampDistance || transform.position.z <= centerPoint.z - clampDistance)
        {
            windDirection.z = -windDirection.z;
        }
        rb.velocity = windDirection * speed;
    }
}