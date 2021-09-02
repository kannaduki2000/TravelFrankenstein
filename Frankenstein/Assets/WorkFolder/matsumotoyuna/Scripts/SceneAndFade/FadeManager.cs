using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    float fadeSpeed = 0.001f; //遷移速度
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

    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alpha = fadeImage.color.a;
    }

    private void Awake()
    {
        DontDestroyOnLoad(canvas);
        //無限増殖編
    }

    // Update is called once per frame
    void Update()
    {
        //それぞれのフェードに飛ぶお
        if (isFadein)
        {
            StartFadeIn();
        }

        if (isFadeout)
        {
            StartFadeOut();
        }

        if (isWhiteout)
        {
            StartWhiteOut();
        }
    }

    void StartFadeIn()
    {
        //フェードイン
        alpha -= fadeSpeed;
        SetAlpha();

        if (alpha <= 0)
        {
            isFadein = false;
            fadeImage.enabled = false;

            if (sc.Map12 == true)
            {
                //ロゴとMap1の間
                isFadeout = true;
                sc.Map12 = false;
                sc.Map1 = true;
            }
        }
    }

    public void StartFadeOut()
    {
        //黒くフェードアウト
        fadeImage.enabled = true;
        alpha += fadeSpeed;
        SetAlpha();

        if (alpha >= 1)
        {
            //同時にフェードインも済ませちまうze
            isFadeout = false;
            isFadein = true;

            if (sc.Title == true)
            {
                //タイトルへ
                Debug.Log("TentativeTitle");
                SceneManager.LoadScene("TentativeTitle");
                sc.Title = false;
            }

            if (sc.Tutorial == true)
            {
                //チュートリアルへ
                Debug.Log("handa");//半田:シーンの変更
                SceneManager.LoadScene("handa");//半田:シーンの変更
                sc.Tutorial = false;
            }

            if (sc.Map1 == true)
            {
                //Map1へ
                Debug.Log("Stag1");//半田:シーンの変更
                SceneManager.LoadScene("Stag1");//半田:シーンの変更
                sc.Map1 = false;
            }

            if (sc.Map2 == true)
            {
                //Map2へ
                //本来はマップ3に遷移
                Debug.Log("Main_Stage2");
                SceneManager.LoadScene("Main_Stage2");
                sc.Map2 = false;
            }
        }
    }

    void StartWhiteOut()
    {
        //白くフェードアウト
        alpha += fadeSpeed;
        SetAlpha();

        if (alpha >= 1)
        {
            //同時にフェードインも済ましマスオ
            isFadein = true;
            isWhiteout = false;

            if (sc.Logo == true)
            {
                //タイトルロゴ→Map1へ
                SceneManager.LoadScene("TitleLogo");
                sc.Logo = false;
                sc.Map12 = true;
            }
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpha);
    }
}
