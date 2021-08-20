using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTitleEvent : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 3.0f;
    [SerializeField] private float fadeDisplayTime = 3.0f;
    [SerializeField] private Image fadeImage = null;
    [SerializeField] private PlayerController player;

    public bool fadeOutFlag = false;
    private bool trigger = true;

    Color myColor;
    Color color_zero;

    float alpha = 0;

    System.Action _callback;



    private void Awake()
    {
        myColor = fadeImage.color;
        color_zero = new Color(myColor.r, myColor.g, myColor.b, 0);
    }


    void Start()
    {
        //player.PlayerNotMove();
    }



    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    // ���̃t���O���J�����ړ��Ńt�����P�����ڂ������true�ɂ����subTitle�\������܂��A����̓R�}���h�Ŏ��s���܂�
        //    EventFlagManager.Instance.SetFlagState(EventFlagName.stage1Title, true);
        //}



        if (EventFlagManager.Instance.GetFlagState(EventFlagName.stage1Title) && trigger)
        {
            // �A���ŃC�x���g�����Ȃ��悤�ɂ���
            trigger = false;
            Fade("in" , ()=> fadeOutFlag = true);
        }

        if (fadeOutFlag)
        {
            fadeDisplayTime -= Time.deltaTime;
            if (fadeDisplayTime < 0)
            {
                fadeOutFlag = false;
                Fade("out", ()=> player.PlayerMove());
            }
        }
    }


    IEnumerator FadeOut()
    {
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

    IEnumerator FadeIn()
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
        yield break;
    }

    public void Fade(string name, System.Action callback = null)
    {
        _callback = callback;
        if (name == "out")
        {
            //���t�F�[�h�C��
            StartCoroutine(FadeOut());
        }
        else if (name == "in")
        {
            //���t�F�[�h�A�E�g
            StartCoroutine(FadeIn());
        }
    }
}
