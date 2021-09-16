using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFlag : MonoBehaviour
{
    Transform Bone; 　// Boneの位置情報取得

    public bool isBone = false;　　// スイッチ


    // Boneタグがついている物に触れたら
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bone")
        {
            isBone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bone")
        {
            isBone = false;
        }
    }
}
