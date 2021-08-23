using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInactive : MonoBehaviour
{
    public GameObject enem1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown("e"))
        {
            enem1.SetActive (true);
        }
    }
}
