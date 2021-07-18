using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomActive : MonoBehaviour
{ 
    public bool Zoominout;

    public Vector3[] Target;

    public Camera Cam;

    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;


    }

    // Update is called once per frame
    void Update()
    {
        
        if(Zoominout)
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize,5,Speed);
        }
        else
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize,6,Speed);
        }
    }
}
