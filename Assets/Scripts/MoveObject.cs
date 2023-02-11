using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{   
    // movement variables
    public Vector3 movement;
    public Vector3 movementMax;
    public Vector3 startPosition;
    public int movementDirection = 1;
    public bool movementCap = true;

    // rotation variables
    public Vector3 rotation;
    public Vector3 rotationMax;
    public Vector3 startRotation;
    public int rotationDirection = 1;
    public bool rotationCap = true;

    // Loads start position and rotation
    void Start()
    {
        startPosition = movementDirection > 0 ? transform.position : transform.position - movementMax;
        startRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
    // Update is called once per frame
    void Update()
    {
        // checks if movement is set
        if (movement.x != 0 || movement.y != 0 || movement.z != 0)
        {
            // moves object in direction
            transform.position += movement * Time.deltaTime * movementDirection;

            if (movementCap)
            {
                // checks if it went past maximum
                if (transform.position.x > startPosition.x + movementMax.x || transform.position.y > startPosition.y + movementMax.y || transform.position.z > startPosition.z + movementMax.z)
                {
                    movementDirection = -1;
                }
                else if (transform.position.x < startPosition.x || transform.position.y < startPosition.y || transform.position.z < startPosition.z)
                {
                    movementDirection = 1;
                }
            }
        }

        // checks if rotation is set
        if (rotation.x != 0 || rotation.y != 0 || rotation.z != 0)
        {
            transform.Rotate(rotation * Time.deltaTime * rotationDirection, Space.Self);

            // checks if there is a limit on rotation
            if (rotationCap)
            {
                // checks if it went past maximum
                if (transform.rotation.x > startRotation.x + rotationMax.x || transform.rotation.y > startRotation.y + rotationMax.y || transform.rotation.z > startRotation.z + rotationMax.z)
                {
                    transform.rotation = new Quaternion(startRotation.x + rotationMax.x, startRotation.y + rotationMax.y, startRotation.z + rotationMax.z, transform.rotation.w);
                }
                else if (transform.rotation.x < startRotation.x || transform.rotation.y < startRotation.y || transform.rotation.z < startRotation.z)
                {
                    transform.rotation = new Quaternion(startRotation.x, startRotation.y, startRotation.z, 0);
                }

                // changes direction
                if (transform.rotation == new Quaternion(startRotation.x + rotationMax.x, startRotation.y + rotationMax.y, startRotation.z + rotationMax.z, 0))
                {
                    rotationDirection = -1;
                }
                else if (transform.rotation == new Quaternion(startRotation.x, startRotation.y, startRotation.z, transform.rotation.w))
                {
                    rotationDirection = 1;
                }
            }
        }    
    }
}