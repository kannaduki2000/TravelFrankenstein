using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TextArray
{
    [TextArea(3, 8)] public string[] ChildText;
}


public class TextController : MonoBehaviour
{
    public PlayerController player;
    
    public TextArray[] ParentTexts;

    public Text text;

    private float time;
    public float textTime;          // Text送りの時間
    public float loadTime;          // 次の文章に切り替わるまでの時間
    private int textNum = 0;        // 表示中のテキスト
    private int parentArrayNum = 0; // 親配列の番号
    private int childArrayNum = 0;  // 子配列の番号
    private bool loadFlag = false;  // ロード中かどうかのフラグ
    public bool textFlag = false;   // Text送り中かどうかのフラグ
    private bool aaa = false;
    public bool active = false;     // テキスト送り中以外は非アクティブ

    private System.Action callback = null; 

    void Start()
    {
        text = GetComponent<Text>();
        SetTextActive(false);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    SetTextActive(true);
        //}


        if (active == false) { return; }


        // 全部のテキスト送りが終わったら非表示にする
        if (ParentTexts.Length == parentArrayNum)
        {
            gameObject.SetActive(false);
        }

        // 1イベント分のテキスト送りが終わったら次の文章をセットして非表示にする
        if (ParentTexts[parentArrayNum].ChildText.Length == childArrayNum)
        {
            if (aaa) { }
            loadFlag = true;
            time += Time.deltaTime;
            if (loadTime < time)
            {
                loadFlag = false;
                parentArrayNum++;
                if (parentArrayNum == 1)
                {
                    EventFlagManager.Instance.SetFlagState(EventFlagName.textSystem, true);
                    player.PlayerSetAnnounceImage(AnnounceName.T_Leftstick_Move);
                    //AnnounceImageManager.Instance.SetAnnounceImage(player.AnnounceImage.sprite, AnnounceName.T_Leftstick_Move);
                }
                else if (parentArrayNum == 2)
                {
                    if (callback != null) callback();
                }
                childArrayNum = 0;
                textNum = 0;
                active = false;
                SetTextActive(false);
                player.PlayerMove();
                textFlag = false;
                return;
            }
        }

        // 文章の最後まで来たらロードさせて次の文章のセットをする
        if (ParentTexts[parentArrayNum].ChildText[childArrayNum].Length == textNum - 1)
        {
            loadFlag = true;
            time += Time.deltaTime;
            if (loadTime < time)
            {
                loadFlag = false;
                childArrayNum++;
                textNum = 0;
                return;
            }
        }


        // 次の文章までのロード時間がくるまで待機
        if (loadFlag) { return; }

        // テキスト送り
        time += Time.deltaTime;
        if (textTime < time)
        {
            time = 0;
            DisplaySentence();
            textNum++;
        }
    }

    /// <summary>
    /// テキスト送り
    /// </summary>
    public void DisplaySentence()
    {
        textFlag = true;
        if (ParentTexts[parentArrayNum].ChildText[childArrayNum].Length == textNum - 1) { return; }
        text.text = ParentTexts[parentArrayNum].ChildText[childArrayNum].Substring(0, textNum);
    }

    /// <summary>
    /// Textの表示/非表示
    /// </summary>
    /// <param name="value"></param>
    public void SetTextActive(bool value, System.Action _callback = null)
    {
        callback = _callback;
        text.enabled = value;
        active = value;
    }

}
