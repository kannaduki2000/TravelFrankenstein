using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamecontrol : MonoBehaviour
{
    public GameObject objectToDisable;
    public static bool disabled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ///////////////////////////
        //to disappear 
        if(disabled)
        objectToDisable.SetActive(false);
        else
        objectToDisable.SetActive(true);
        ///////////////////////////
    }
}
