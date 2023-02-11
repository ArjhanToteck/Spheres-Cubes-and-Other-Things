using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // loads player
    public Transform player;

    // offset of camera
    public Vector3 offset;

    void Update()
    {
        float playerExtraSize = player.localScale.y <= 1 ? 0 : player.localScale.y - 1;
        transform.position = player.position + new Vector3((offset.y * (Physics.gravity.x / -9.81f)) + playerExtraSize * (Physics.gravity.x / -9.81f), (offset.y * (Physics.gravity.y / -9.81f)) + (playerExtraSize * offset.y) * (Physics.gravity.y / -9.81f), offset.z + (playerExtraSize * offset.z));
    }
}
