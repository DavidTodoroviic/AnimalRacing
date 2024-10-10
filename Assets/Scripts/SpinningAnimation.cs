using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFloatSpin : MonoBehaviour
{
    public float bobbingSpeed = 0.5f;
    public float bobbingAmount = 0.5f;
    public float rotationSpeed = 50f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Bobbing();
        Spinning();
    }

    void Bobbing()
    {
        float newY = startPosition.y + Mathf.Abs(Mathf.Sin(Time.time * bobbingSpeed)) * bobbingAmount;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    void Spinning()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
