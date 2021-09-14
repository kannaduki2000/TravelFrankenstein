using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public Transform targetToFollow;
   
    public float LeftX = -16.88f;
    public float RightX = 87f;
    public float UpY = -10f;
    public float DownY = -5.024302f;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, LeftX, RightX),
            Mathf.Clamp(targetToFollow.position.y, UpY, DownY),
            transform.position.z);
    }
}
