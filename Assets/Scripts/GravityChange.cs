using UnityEngine;

public class GravityChange : MonoBehaviour
{
    public Vector3 gravityChange;

    void Start()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    public void changeGravity()
    {
        Physics.gravity = gravityChange;
    }
}
