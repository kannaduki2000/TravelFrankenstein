using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DualShockInput;

public class FadeControl : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 1.0f;
    [SerializeField] private Image fadeImage = null;
    public SceneChange sceneChange;

    Color myColor;
    Color color_zero;

    float alpha = 0;

    System.Action _callback;

    public bool hiyoko = false;

    private void Awake()
    {
        myColor = fadeImage.color;
        color_zero = new Color(myColor.r, myColor.g, myColor.b, 0);
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || DSInput.PushDown(DSButton.Circle)) && hiyoko == false)
        {
            Fade("out", ()=> sceneChange.SceneSwitching("MainTutorial"));
            hiyoko = true;
        }
    }

    IEnumerator FadeIn()
    {
        //EventFlagManager.Instance.SetFlagState(EventFlagName.isFade, true);
        alpha = fadeImage.color.a;
        while (0 < alpha)
        {
            fadeImage.color = new Color(myColor.r, myColor.g, myColor.b, alpha);
            alpha -= Time.deltaTime / fadeSpeed;
            yield return null;
        }
        if (_callback != null) _callback();
        EventFlagManager.Instance.SetFlagState(EventFlagName.isFade, false);
        yield break;
    }

    IEnumerator FadeOut()
    {
        EventFlagManager.Instance.SetFlagState(EventFlagName.isFade, true);
        alpha = fadeImage.color.a;
        while (alpha < 1)
        {
            fadeImage.color = new Color(myColor.r, myColor.g, myColor.b, alpha);
            alpha += Time.deltaTime / fadeSpeed;
            yield return null;
        }
        if (_callback != null) _callback();
        //EventFlagManager.Instance.SetFlagState(EventFlagName.isFade, false);
        yield break;
    }

    IEnumerator WhiteFadeIn()
    {
        //EventFlagManager.Instance.SetFlagState(EventFlagName.isFade, true);
        Debug.Log("honntouno fadein takoyaki");
        alpha = fadeImage.color.a;
        while (0 < alpha)
        {
            fadeImage.color = new Color(1, 1, 1, alpha);
            alpha -= Time.deltaTime / fadeSpeed;
            yield return null;
        }
        if (_callback != null) _callback();
        EventFlagManager.Instance.SetFlagState(EventFlagName.isFade, false);
        yield break;
    }

    IEnumerator WhiteFadeOut()
    {
        EventFlagManager.Instance.SetFlagState(EventFlagName.isFade, true);
        alpha = fadeImage.color.a;
        while (alpha < 1)
        {
            fadeImage.color = new Color(1, 1, 1, alpha);
            alpha += Time.deltaTime / fadeSpeed;
            yield return null;
        }
        if (_callback != null) _callback();
        //EventFlagManager.Instance.SetFlagState(EventFlagName.isFade, false);
        yield break;
    }

    public void Fade(string name, System.Action callback = null)
    {
        _callback = callback;
        if (name == "in")
        {
            //黒フェードイン
            StartCoroutine(FadeIn());
        }
        else if (name == "out")
        {
            //黒フェードアウト
            StartCoroutine(FadeOut());
        }
        else if (name == "win")
        {
            //白フェードイン
            StartCoroutine(WhiteFadeIn());
        }
        else if (name == "wout")
        {
            //白フェードアウト
            StartCoroutine(WhiteFadeOut());
        }
    }
}
