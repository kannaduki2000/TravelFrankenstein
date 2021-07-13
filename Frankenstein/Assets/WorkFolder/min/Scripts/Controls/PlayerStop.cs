using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop : MonoBehaviour
{
    public int nomove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePly();
        MyInput();
    }

    void MovePly()
    {
        transform.Translate (Vector3.right * nomove * Time.deltaTime);
    }

    void MyInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            nomove = 0; //Player Stop from moving
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            nomove = 3; //Player moving
        }
    }
}
