using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box1 : MonoBehaviour
{
    public GameObject uiButton;
    [SerializeField] private bool touchFlag = false;
    [SerializeField] private bool enemyTouchFlag = false;
    


    public GameObject box;

    

    void update()
    { 


        if (touchFlag || enemyTouchFlag)
        {
            // ?\??
            uiButton.SetActive(true);

            // ?d?C??????
            if (Input.GetKeyDown(KeyCode.Return))
            {

                if (enemyTouchFlag)
                {



                    uiButton.SetActive(false);
                }

            }

            // ???b?N???F?q:HP?\??????Object?????????????????I??HP?o?[?????\??????????
            else
            {
                uiButton.SetActive(false);
            }

        }
    }

    
    

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                touchFlag = true;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                touchFlag = false;
                uiButton.SetActive(false);
            }
        }
}





