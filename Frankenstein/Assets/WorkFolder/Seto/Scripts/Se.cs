using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Se : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
     //スペースキー
     if (Input.GetKeyDown (KeyCode.Space))
     {
         //sound1を鳴らす
         audioSource.PlayOneShot(sound1);
     }   
     //左キー
     if (Input.GetKeyDown(KeyCode.LeftArrow))
     {
         //sound2を鳴らす
         audioSource.PlayOneShot(sound2);
     }   
     //右キー
     if (Input.GetKeyDown (KeyCode.RightArrow))
     {
         //sound3を鳴らす
         audioSource.PlayOneShot(sound3);
     }   
    }


    public void Se1(){
        audioSource.PlayOneShot(sound1);

    }
}
