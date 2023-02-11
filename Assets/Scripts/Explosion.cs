using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    public Rigidbody rb;
    public Rigidbody playerRb;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FindObjectOfType<GameManager>().gameEnded)
        {
            explosion.gameObject.SetActive(true);
            rb.position = playerRb.position;
        }
    }
}
