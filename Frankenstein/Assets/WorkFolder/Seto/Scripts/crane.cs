using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crane : MonoBehaviour
{
    [SerializeField] GameObject enem;
    [SerializeField] GameObject enem1;
    private Animator anim;

    [SerializeField] dropdown drop;

    public bool craneMove = false;

    //[SerializeField] PlayerController player;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (craneMove == true)
        {
            AnimationControl();
        }
    }
    public void AnimationControl()
    {
        //"e"キーを押したときの処
        //エネミーを非アクティブ
        enem.SetActive(false);
        //エネミー1をアクティブ
        enem1.SetActive(true);
        //アニメーションCraneを再生
        anim.Play("Crane");

        Invoke("CrashFrame", 1.5f);

    }

    private void CrashFrame()
    {
        drop.foll = true;
    }
}
