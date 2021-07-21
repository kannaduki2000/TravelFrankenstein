using UnityEngine;
using UnityEngine.UI;

public class ElectricItem : MonoBehaviour
{
    public int Power { get; set; } = 0;                 // 消費電力
    public bool ChargeFlag { get; set; } = false;       // 電気を入れてあるかどうか　OnOff可能
    public bool IsCharge { get; set; } = false;         // Playerが充電できるか
    public bool IsChargeEvent { get; set; } = false;    // Event実行済かどうか　Onのみ
    public Sprite[] AnnounceImage { get; set; } = null; // 指示表示画像
    public bool IsThrow { get; set; } = false;          // 投げれるObjectかどうか

    public virtual void Event() { }
    public virtual void ChargeEvent() { }

}
