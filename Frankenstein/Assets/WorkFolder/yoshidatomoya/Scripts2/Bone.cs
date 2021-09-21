using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public bool isBone = false;

    // 骨が地面に触れたら
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に触れたら
        if (collision.gameObject.tag == "Ground")
        {
            isBone = true;
        }
    }

    // 骨が地面から離れたら
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 地面から離れたら
        if (collision.gameObject.tag == "Ground")
        {
            isBone = false;
        }
    }


}
