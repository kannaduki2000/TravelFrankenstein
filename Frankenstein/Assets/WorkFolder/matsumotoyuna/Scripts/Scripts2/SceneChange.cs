using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //召喚
    public FadeManager fm;

    //色々
    public bool Title = false;
    public bool Tutorial = false;
    public bool Map1 = false;
    public bool Logo = false;
    public bool Map2 = false;
    Rigidbody2D rigid2D;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //判定の場所を通過したら発生
        if(collision.gameObject.tag == "GoTitleLogo")
        {
            Logo = true;
        }

        if(collision.gameObject.tag == "GoMap2")
        {
            Map2 = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(初回とゲームオーバー時)
        {
            タイトル画面表示
            →(ゲームオーバー判定が来たらシーンチェンジタイトル)
        }
        */

        //タイトル画面限定処理
        if(SceneManager.GetActiveScene().name == "TentativeTitle")
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                fm.isFadeout = true;
                Tutorial = true;
            }
        }

        if (Map1 == true && SceneManager.GetActiveScene().name == "TentativeMap1")
        {
            fm.isFadein = true;
        }

        if (Logo == true)
        {
            fm.isWhiteout = true;
        }

        if (Map1 == true)
        {
            fm.isWhiteout = true;
        }

        if(Map2 == true)
        {
            fm.isFadeout = true;
        }

        /*
        if(タイトルの後)
        {
            タイトルはフェードアウトかねぇ…？
            fm.isFadeout = !fm.isFadeout;
            タイトルの後にフェードインでチュートリアル画面表示
            SceneManager.LoadScene("チュートリアル");
            fm.isFadein = !fm.isFadein;
        }
        */

        /*
        if(右端まで行くと)
        {
            白にフェードアウト
            fm.isWhiteout = !fm.isWhiteout;
            
            フェードインでロゴ
            fm.isFadein = !fm.isFadein;のロゴ

            (この後に)
            フェードインで空
            SceneManager.LoadScene("マップ1");
            fm.isFadein = !fm.isFadein;の空
        }
        */

        /*
        if(ショッピングモール入口に侵入)
        {
            黒くフェードアウト
            fm.isFadeout = !fm.isFadeout;
            SceneManager.LoadScene("マップ2?");
        }
        */
    }
}
