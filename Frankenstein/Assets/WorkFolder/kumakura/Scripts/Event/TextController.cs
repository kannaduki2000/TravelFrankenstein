using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TextArray
{
    public string[] ChildText;
}


public class TextController : MonoBehaviour
{
    public PlayerController player;
    
    public TextArray[] ParentTexts;

    public Text text;

    private float time;
    public float textTime;          // Text����̎���
    public float loadTime;          // ���̕��͂ɐ؂�ւ��܂ł̎���
    private int textNum = 0;        // �\�����̃e�L�X�g
    private int parentArrayNum = 0; // �e�z��̔ԍ�
    private int childArrayNum = 0;  // �q�z��̔ԍ�
    private bool loadFlag = false;  // ���[�h�����ǂ����̃t���O
    public bool textFlag = false;   // Text���蒆���ǂ����̃t���O
    private bool aaa = false;
    public bool active = false;     // �e�L�X�g���蒆�ȊO�͔�A�N�e�B�u

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


        // �S���̃e�L�X�g���肪�I��������\���ɂ���
        if (ParentTexts.Length == parentArrayNum)
        {
            gameObject.SetActive(false);
        }

        // 1�C�x���g���̃e�L�X�g���肪�I������玟�̕��͂��Z�b�g���Ĕ�\���ɂ���
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

        // ���͂̍Ō�܂ŗ����烍�[�h�����Ď��̕��͂̃Z�b�g������
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


        // ���̕��͂܂ł̃��[�h���Ԃ�����܂őҋ@
        if (loadFlag) { return; }

        // �e�L�X�g����
        time += Time.deltaTime;
        if (textTime < time)
        {
            time = 0;
            DisplaySentence();
            textNum++;
        }
    }

    /// <summary>
    /// �e�L�X�g����
    /// </summary>
    public void DisplaySentence()
    {
        textFlag = true;
        if (ParentTexts[parentArrayNum].ChildText[childArrayNum].Length == textNum - 1) { return; }
        text.text = ParentTexts[parentArrayNum].ChildText[childArrayNum].Substring(0, textNum);
    }

    /// <summary>
    /// Text�̕\��/��\��
    /// </summary>
    /// <param name="value"></param>
    public void SetTextActive(bool value, System.Action _callback = null)
    {
        callback = _callback;
        text.enabled = value;
        active = value;
    }

}
