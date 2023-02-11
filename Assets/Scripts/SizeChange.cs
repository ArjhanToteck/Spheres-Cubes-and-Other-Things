using UnityEngine;

public class SizeChange : MonoBehaviour
{
    public int changeType = 1;

    public Material changeSizeLarge;
    public Material changeSizeSmall;

    void Start()
    {
        // changes material depending on changeType
        GetComponent<Renderer>().material = changeType > 0 ? changeSizeLarge : changeSizeSmall;
    }
}
