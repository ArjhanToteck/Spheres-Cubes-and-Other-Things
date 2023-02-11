using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMenu : MonoBehaviour
{
    public Rigidbody rb;

    // player speed
    public float xSpeed = 500f;
    public float zSpeed = 175f;
    public float zMaxVelocity = 4000f;

    // Update is called once per frame
    void FixedUpdate()
    {
        // perpetual forward speed
        rb.AddForce(0, 0, zSpeed * Time.deltaTime);

        // caps forward speed
        rb.velocity = (rb.velocity.z > zMaxVelocity) ? new Vector3(rb.velocity.x, rb.velocity.y, zMaxVelocity) : rb.velocity;
    }
}
