using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finding : MonoBehaviour
{
     public float rotationSpeed = 45.0f; // Adjust the speed of the pendulum
    public float rotationAngle = 45.0f; // Adjust the maximum angle of rotation

    private float initialRotation;
    private int direction = 1;

    private void Start()
    {
        initialRotation = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        // Calculate the rotation angle based on a sine wave
        float angle = initialRotation + rotationAngle * Mathf.Sin(Time.time * rotationSpeed * direction);

        // Apply rotation to the GameObject
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Reverse direction when reaching the maximum angle
        if (angle >= initialRotation + rotationAngle || angle <= initialRotation - rotationAngle)
        {
            direction *= -1;
        }
    }

}
