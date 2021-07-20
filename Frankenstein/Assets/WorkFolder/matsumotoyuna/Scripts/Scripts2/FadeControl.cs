using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        if (Input.GetKeyDown(KeyCode.Return) && hiyoko == false)
        {
            Fade("out", ()=> sceneChange.SceneSwitching("handa"));//���c�Fscene���̕ύX
            hiyoko = true;
        }
    }

    IEnumerator FadeIn()
    {
        alpha = fadeImage.color.a;
        while (0 < alpha)
        {
            fadeImage.color = new Color(myColor.r, myColor.g, myColor.b, alpha);
            alpha -= Time.deltaTime / fadeSpeed;
            yield return null;
        }
        if (_callback != null) _callback();
        yield break;
    }

    IEnumerator FadeOut()
    {
        alpha = fadeImage.color.a;
        while (alpha < 1)
        {
            fadeImage.color = new Color(myColor.r, myColor.g, myColor.b, alpha);
            alpha += Time.deltaTime / fadeSpeed;
            yield return null;
        }
        if (_callback != null) _callback();
        yield break;
    }

    IEnumerator WhiteFadeIn()
    {
        Debug.Log("honntouno fadein takoyaki");
        alpha = fadeImage.color.a;
        while (0 < alpha)
        {
            fadeImage.color = new Color(1, 1, 1, alpha);
            alpha -= Time.deltaTime / fadeSpeed;
            yield return null;
        }
        if (_callback != null) _callback();
        yield break;
    }

    IEnumerator WhiteFadeOut()
    {
        alpha = fadeImage.color.a;
        while (alpha < 1)
        {
            fadeImage.color = new Color(1, 1, 1, alpha);
            alpha += Time.deltaTime / fadeSpeed;
            yield return null;
        }
        if (_callback != null) _callback();
        yield break;
    }

    public void Fade(string name, System.Action callback = null)
    {
        _callback = callback;
        if (name == "in")
        {
            //���t�F�[�h�C��
            StartCoroutine(FadeIn());
        }
        else if (name == "out")
        {
            //���t�F�[�h�A�E�g
            StartCoroutine(FadeOut());
        }
        else if (name == "win")
        {
            //���t�F�[�h�C��
            StartCoroutine(WhiteFadeIn());
        }
        else if (name == "wout")
        {
            //���t�F�[�h�A�E�g
            StartCoroutine(WhiteFadeOut());
        }
    }
}
