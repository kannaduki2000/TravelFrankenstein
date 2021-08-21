using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -38.5f, 38.5f), // モック版熊倉:ここらへん少し書き換えました
            Mathf.Clamp(targetToFollow.position.y, 10f, 10f),
            transform.position.z);
    }
}
