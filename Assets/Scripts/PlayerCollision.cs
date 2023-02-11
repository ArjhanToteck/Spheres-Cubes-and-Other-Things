using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem explosion;
    public Rigidbody explosionRb;

    public float bounceSpeed = 8f;
    public float sizeIncrement = 2f;

    void OnCollisionEnter(Collision collision)
    {
        // obstacle
        if(collision.collider.tag == "Obstacle" && FindObjectOfType<GameManager>().gameEnded == false && rb.position.z <= collision.transform.position.z)
        {
            rb.velocity /= 5;
            FindObjectOfType<SoundManager>().explosion();

            // executes GameOver function in GameManager
            FindObjectOfType<GameManager>().GameOver("You ran into an obstacle");

            // explosion
            explosionRb.position = rb.position;
            explosion.Clear();
            explosion.Play();
        }
        
        // bounce
        if (collision.collider.tag == "Bounce")
        {
            rb.velocity = new Vector3(bounceSpeed * (Physics.gravity.x / -9.81f), bounceSpeed * (Physics.gravity.y / -9.81f), 0);
            FindObjectOfType<SoundManager>().bounce();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // gravityChange
        if (collision.tag == "GravityChange")
        {
            // calls changeGravity in collision
            if (!!collision.GetComponent<GravityChange>())
            {
                collision.GetComponent<GravityChange>().changeGravity();
            }
            FindObjectOfType<SoundManager>().powerUp();
        }

        // sizeChange
        if (collision.tag == "SizeChange")
        {
            // changes size on player depending on collision's changeType variable
            if (!!collision.GetComponent<SizeChange>())
            {
                rb.gameObject.transform.localScale = collision.GetComponent<SizeChange>().changeType > 0 ? rb.gameObject.transform.localScale * sizeIncrement : rb.gameObject.transform.localScale / sizeIncrement;
                FindObjectOfType<SoundManager>().powerUp();
            }
        }

        // sizeChange
        if (collision.tag == "Portal")
        {
            rb.position = new Vector3(0, 1, 0);
            rb.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}