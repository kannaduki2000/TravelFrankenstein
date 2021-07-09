using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsGimmick : MonoBehaviour
{
  
    public GameObject stairs;
    // Start is called before the first frame update
    void Start()
    {  
        stairs = GameObject.Find("Stairs");
        stairs.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    //仮アニメーション作成→挿入
    //電気を入れるの代わりにPを押す
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                stairs.gameObject.SetActive(true);

            }
        }
    }


    //アニメーション開始
    //アニメーション終了

}
