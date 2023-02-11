using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    // player speed
    public float xSpeed = 500f;
    public float zSpeed = 175f;
    public float zMaxVelocity = 4000f;
    List<Collider> boundaries = new List<Collider>();

   void OnTriggerExit(Collider collision)
    {
        // detects if fallen out of boundaries
        if (collision.tag == "Boundaries" && FindObjectOfType<GameManager>().gameEnded == false && Physics.gravity != new Vector3(0, -9.81f, 0) && boundaries.Contains(collision))
        {
            boundaries.Remove(collision);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // detects if fallen out of boundaries
        if (collision.tag == "Boundaries" && FindObjectOfType<GameManager>().gameEnded == false && !boundaries.Contains(collision))
        {
            boundaries.Add(collision);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // perpetual forward speed
        rb.AddForce(0, 0, zSpeed * Time.deltaTime);

        // caps forward speed
        rb.velocity = (rb.velocity.z > zMaxVelocity) ? new Vector3(rb.velocity.x, rb.velocity.y, zMaxVelocity) : rb.velocity;

        // controls
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-xSpeed * Time.deltaTime * (Physics.gravity.y / -9.81f), 1 * (Physics.gravity.x / -9.81f), 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(xSpeed * Time.deltaTime * (Physics.gravity.y / -9.81f), -1 * (Physics.gravity.x / -9.81f), 0, ForceMode.VelocityChange);
        }

        // falling off edge with normal gravity
        if ((rb.position.y < -3f && FindObjectOfType<GameManager>().gameEnded == false && Physics.gravity == new Vector3(0, -9.81f, 0)) || (Physics.gravity != new Vector3(0, -9.81f, 0) && boundaries.Count == 0 && FindObjectOfType<GameManager>().gameEnded == false))
        {
            // executes GameOver function in GameManager
            FindObjectOfType<GameManager>().GameOver("You fell off the edge");
        }
    }
}
