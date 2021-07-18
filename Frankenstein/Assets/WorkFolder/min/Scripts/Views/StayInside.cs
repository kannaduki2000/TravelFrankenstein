using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    
    public float LeftX = -53f;
    public float RightX = 128f;
    public float DownY = -100f;
    public float UpY = 21f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LeftX, RightX),
            Mathf.Clamp(transform.position.y, UpY, DownY), transform.position.z); 
    }
}
