using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsAnim : MonoBehaviour
{
    Animator anim;
    GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stairs()
    {
        //アニメーションが再生
        //ループを切っているので一回のみ再生。
        //判定は滅茶苦茶ある。
        anim.SetBool("Stairs", true);
    }

    //アニメーションイベントにて設定。
    //再生後、透明な壁が非表示になる。
    private void Wall()
    {
        wall = GameObject.Find("TransparentWall");
        wall.SetActive(false);
        Debug.Log("壁、非表示");
    }

    
}
