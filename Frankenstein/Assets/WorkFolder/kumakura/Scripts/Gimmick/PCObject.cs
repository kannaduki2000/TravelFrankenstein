using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DualShockInput;

public class PCObject : ElectricItem
{
    [SerializeField] private int power;
    [SerializeField] private Image announceObject;      // �w���\���摜������Object
    [SerializeField] private Image image;               // ���|�[�g������Object�@�\������ꏊ���Ⴄ�̂ŕ����܂���
    [SerializeField] private ImageData imageData; 
    [SerializeField] private Sprite eveReport;
    [SerializeField] private PlayerController player;

    // ������ւ񔋌�����̏����p�̕ϐ�
    [SerializeField] SpriteRenderer PC_SpriteRenderer;  // PC���
    [SerializeField] Sprite PC_ON_Sprite;               // On�摜
    [SerializeField] Sprite PC_OFF_Sprite;              // Off�摜
    bool PCSwitchFlag;


    void Start()
    {
        Power = power;
        IsThrow = false;
        IsCharge = true;
        imageData = FindObjectOfType<ImageData>();
    }

    void Update()
    {
        if (IsChargeEvent)
        {
            if (EventFlagManager.Instance.GetFlagState(EventFlagName.text_Eve) == false)
            {
                announceObject.enabled = false;
                // �Z�{�^���Ή��\��
                if (Input.GetKeyDown(KeyCode.Return) || DSInput.PushDown(DSButton.Circle))
                {
                    // ���|�[�g�̔�\���A�e�L�X�g�C�x���g�̎��s
                    EveReportUnEnabled();
                }
            }
        }
        PCSwitchFlag = ChargeFlag;
        if (ChargeFlag)
        {
            announceObject.sprite = imageData.GetAnnounceImage(AnnounceName.T_SquareButton_Absorption);
            PCSwitch();
        }
        else
        {
            announceObject.sprite = imageData.GetAnnounceImage(AnnounceName.T_SquareButton_Input);
            PCSwitch();
        }
    }

    /// <summary>
    /// �d�C����ꂽ�Ƃ��̃C�x���g
    /// </summary>
    public override void Event()
    {
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.text_Eve)) { return; }
        player.PlayerNotMove();
        // �C���̃��|�[�g�̕\��
        image.enabled = true;
        image.sprite = imageData.GetReportImage(ReportName.eveReport);
    }

    /// <summary>
    /// �[�d�������̃C�x���g
    /// </summary>
    public override void ChargeEvent()
    {
        // ����ŃG�l�~�[�ւ̏[�d�t���O������
        EventFlagManager.Instance.SetFlagState(EventFlagName.electricAabsorption, true);
    }

    public void EveReportUnEnabled()
    {
        image.enabled = false;
        // �e�L�X�g�C�x���g���s���Player��������悤�ɂ���
        //player.textCon.SetTextActive(true, ()=> player.PlayerMove());
        player.textCon.SetTextActive(true, ()=> 
        {
            player.PlayerMove();
            EventFlagManager.Instance.SetFlagState(EventFlagName.text_Eve, true);
            announceObject.enabled = true;
        });
        
    }
    
    #region ��������@PC�̓d���y��Sprite�Ɋւ��鏈��
    // ---------------------------------------------------------------------------------------------------------------------
    private void PCSwitch()         // PC�̓d���Ɋւ���֐�
    {
        if (PCSwitchFlag == false)          // PCSwitchFlag �� false �ł����H
        {
            // YES
            PCSwitch_ON();
        }
        else if (PCSwitchFlag == true)          // PCSwitchFlag �� true �ł����H
        {
            // YES
            PCSwitch_OFF();
        }
    }

    private void PCSwitch_ON()          // PC�ɓd��������֐�
    {
        PC_SpriteRenderer.sprite = PC_ON_Sprite;            // PC_ON_Sprite �ɂ��܂����Ⴈ���˂��Ă������X�v���C�g�� PC_SpriteRenderer �̃X�v���C�g�ɓ����
        //PCSwitchFlag = true; // ������U�����܂�
    }

    private void PCSwitch_OFF()         // PC�̓d�����؂��֐�
    {
        PC_SpriteRenderer.sprite = PC_OFF_Sprite;            // PC_ON_Sprite �ɂ��܂����Ⴈ���˂��Ă������X�v���C�g�� PC_SpriteRenderer �̃X�v���C�g�ɓ����
        //PCSwitchFlag = false; // ������U�����܂�
    }
    // ---------------------------------------------------------------------------------------------------------------------
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            announceObject.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            announceObject.enabled = false;
        }
    }
}
