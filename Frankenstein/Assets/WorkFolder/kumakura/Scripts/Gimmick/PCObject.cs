using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DualShockInput;

public class PCObject : ElectricItem
{
    [SerializeField] private int power;
    [SerializeField] private Image announceObject;      // 指示表示画像を入れるObject
    [SerializeField] private Image image;               // レポートを入れるObject　表示する場所が違うので分けました
    [SerializeField] private ImageData imageData; 
    [SerializeField] private Sprite eveReport;
    [SerializeField] private PlayerController player;

    // ここらへん萩原さんの処理用の変数
    [SerializeField] SpriteRenderer PC_SpriteRenderer;  // PC画面
    [SerializeField] Sprite PC_ON_Sprite;               // On画像
    [SerializeField] Sprite PC_OFF_Sprite;              // Off画像
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
            if (EventFlagManager.Instance.GetFlagState(EventFlagName.textEve) == false)
            {
                announceObject.enabled = false;
                // 〇ボタン対応予定
                if (Input.GetKeyDown(KeyCode.Return) || DSInput.PushDown(DSButton.Circle))
                {
                    // レポートの非表示、テキストイベントの実行
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
    /// 電気を入れたときのイベント
    /// </summary>
    public override void Event()
    {
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.textEve)) { return; }
        player.PlayerNotMove();
        // イヴのレポートの表示
        image.enabled = true;
        image.sprite = imageData.GetReportImage(ReportName.eveReport);
    }

    /// <summary>
    /// 充電した時のイベント
    /// </summary>
    public override void ChargeEvent()
    {
        // これでエネミーへの充電フラグが立つ
        EventFlagManager.Instance.SetFlagState(EventFlagName.electricAabsorption, true);
    }

    public void EveReportUnEnabled()
    {
        image.enabled = false;
        // テキストイベント実行後にPlayerが動けるようにする
        //player.textCon.SetTextActive(true, ()=> player.PlayerMove());
        player.textCon.SetTextActive(true, ()=> 
        {
            player.PlayerMove();
            EventFlagManager.Instance.SetFlagState(EventFlagName.textEve, true);
            announceObject.enabled = true;
        });
        
    }
    
    #region 萩原さん　PCの電源及びSpriteに関する処理
    // ---------------------------------------------------------------------------------------------------------------------
    private void PCSwitch()         // PCの電源に関する関数
    {
        if (PCSwitchFlag == false)          // PCSwitchFlag が false ですか？
        {
            // YES
            PCSwitch_ON();
        }
        else if (PCSwitchFlag == true)          // PCSwitchFlag が true ですか？
        {
            // YES
            PCSwitch_OFF();
        }
    }

    private void PCSwitch_ON()          // PCに電源が入る関数
    {
        PC_SpriteRenderer.sprite = PC_ON_Sprite;            // PC_ON_Sprite にしまっちゃおうねしておいたスプライトを PC_SpriteRenderer のスプライトに入れる
        //PCSwitchFlag = true; // ここ一旦消します
    }

    private void PCSwitch_OFF()         // PCの電源が切れる関数
    {
        PC_SpriteRenderer.sprite = PC_OFF_Sprite;            // PC_ON_Sprite にしまっちゃおうねしておいたスプライトを PC_SpriteRenderer のスプライトに入れる
        //PCSwitchFlag = false; // ここ一旦消します
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
