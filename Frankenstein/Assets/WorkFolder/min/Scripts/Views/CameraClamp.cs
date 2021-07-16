using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;
    private float LeftX = -8.88f;
    private float RightX = 100f;
    private float UpY = -10f;
    private float DownY = -8.024302f;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, LeftX, RightX),
            Mathf.Clamp(targetToFollow.position.y, UpY, DownY),
            transform.position.z);
    }
}
