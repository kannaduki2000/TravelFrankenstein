using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -51.88f, 100f),
            Mathf.Clamp(targetToFollow.position.y, -10f, -8.024302f),
            transform.position.z);
    }
}
