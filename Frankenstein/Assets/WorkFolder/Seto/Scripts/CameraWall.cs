using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWall : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = new Vector2
        (
            //エリア指定して移動
            Mathf.Clamp( transform.position.x + moveX, -8.3f, 8.3f ),
            Mathf.Clamp( transform.position.y + moveY, -5f, 5f )
        );     
    }
}
