using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crane : MonoBehaviour
{
    [SerializeField] GameObject enem;
    [SerializeField] GameObject enem1;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AnimationControl();
    }
      private void AnimationControl()
    {
        //"e"キーを押したときの処理
        if(Input.GetKeyDown("e"))
        {
            //エネミーを非アクティブ
            enem.SetActive(false);
            //エネミー1をアクティブ
            enem1.SetActive(true);
            //アニメーションCraneを再生
            anim.Play("Crane");
        }
    }
}
