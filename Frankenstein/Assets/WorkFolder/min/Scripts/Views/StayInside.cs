using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    [SerializeField]
    private float LeftX = -53f;
    private float RightX = 128f;
    private float UpY = -100f;
    private float DownY = 21f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LeftX, RightX),
            Mathf.Clamp(transform.position.y, UpY, DownY), transform.position.z); 
    }
}
