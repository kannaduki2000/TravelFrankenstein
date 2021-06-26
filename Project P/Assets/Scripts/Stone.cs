using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour
{
    public GameObject uiButton;
    //public GameObject player;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
        if (player != null )
        uiButton.SetActive(false);

        /*if ()
        { 
         
            uiButton.SetActive(true);
        }*/
        
        
        
    }
}
