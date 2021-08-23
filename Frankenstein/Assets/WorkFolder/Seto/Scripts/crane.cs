using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crane : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationControl();
    }

      private void AnimationControl()
    {
        if(Input.GetKeyDown("e"))
        {
            anim.Play("cranemove");
        }
    }
}
