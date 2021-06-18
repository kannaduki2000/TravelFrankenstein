using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    float fadeSpeed = 0.0005f; //遷移速度
    float red, green, blue, alpha;

    //bool型　～3種のフェードを添えて～
    public bool isFadeout = false;
    public bool isFadein = false;
    public bool isWhiteout = false;

    //イメージやキャンバス
    Image fadeImage;

    [SerializeField] private GameObject canvas;

    //秘技：SceneChange召喚
    public SceneChange sc;

    //Dont～の呼びすぎ防止
    private static bool dontDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alpha = fadeImage.color.a;

        if (!dontDestroy)
        {
            DontDestroyOnLoad(canvas);
            dontDestroy = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //それぞれのフェードに飛ぶお
        if(isFadein)
        {
            StartFadeIn();
        }

        if(isFadeout)
        {
            StartFadeOut();
        }

        if(isWhiteout)
        {
            StartWhiteOut();
        }
    }

    void StartFadeIn()
    {
        //フェードイン
        alpha -= fadeSpeed;
        SetAlpha();

        if(alpha <= 0)
        {
            isFadein = false;
            fadeImage.enabled = false;

            /*if(来週の何か == true)
            {
                SceneManager.LoadScene("TentativeMap1");
                sc.Map1 = false;
            }*/
        }
    }

    public void StartFadeOut()
    {
        //黒くフェードアウト
        fadeImage.enabled = true;
        alpha += fadeSpeed;
        SetAlpha();

        if(alpha >= 1)
        {
            //同時にフェードインも済ませちまうze
            isFadeout = false;
            isFadein = true;

            if(sc.Tutorial == true)
            {
                //チュートリアルへ
                SceneManager.LoadScene("TentativeTutorial");
                sc.Tutorial = false;
            }

            if(sc.Title == true)
            {
                //タイトルへ
                SceneManager.LoadScene("TentativeTitle");
                sc.Title = false;
            }

            if(sc.Map1 == true)
            {
                //来週何か作ってね、いや作れ
            }

            if (sc.Map2 == true)
            {
                //Map2へ
                //本来はマップ3に遷移
                SceneManager.LoadScene("TentativeTitle");
                sc.Map2 = false;
            }
        }
    }

    void StartWhiteOut()
    {
        //白くフェードアウト
        alpha += fadeSpeed;
        SetAlpha();

        if(alpha >= 1)
        {
            //同時にフェードインも済ましマスオ
            isFadein = true;
            isWhiteout = false;

            if (sc.Logo == true)
            {
                //タイトルロゴ→Map1へ
                SceneManager.LoadScene("TitleLogo");
                sc.Logo = false;
                isFadeout = true;
                sc.Map1 = true;
            }
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpha);
    }
}
