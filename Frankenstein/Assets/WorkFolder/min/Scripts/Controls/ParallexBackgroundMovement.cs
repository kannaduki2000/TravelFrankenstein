using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackgroundMovement : MonoBehaviour
{
    [SerializeField]
    private Transform mainCameraPosition; // movement camera position

   [SerializeField]
    private float backgroundMovementSpeed;//  background movement
    private float directionX; //movement direction

    //[SerializeField]
    //private float offsetByX = 13f;

    public Vector3 startPos; //StartPoint for background movement
    public Vector3 endPos; // endPoint for Background movement  

    public float moveTime = 0.1f; // second background Movement

    float elapsedTime; //経過時間 (Delay Time)



    // Update is called once per frame
    void Update()
    {
        //directionX = Input.GetAxis("Horizontal") * backgroundMovementSpeed * Time.deltaTime;

       // transform.position = new Vector2(transform.position.x + directionX,
        //  transform.position.y);

       // if (transform.position.x - mainCameraPosition.position.x < -offsetByX)
       //     transform.position = new Vector2(mainCameraPosition.position.x + offsetByX,transform.position.y);

       // else if (transform.position.x - mainCameraPosition.position.x > offsetByX)
         //  transform.position = new Vector2(mainCameraPosition.position.x - offsetByX,transform.position.y);
   //

        //******Background movement setting 
    if(elapsedTime > moveTime)
        {
            return;
        }


    elapsedTime += Time.deltaTime;

    float rate = Mathf.Clamp01(elapsedTime / moveTime);

    Vector3 currentPos = Vector3.Lerp(startPos, endPos, rate);
    gameObject.transform.position = currentPos;
    
    }

}
