using UnityEngine;
using UnityEngine.UI;

public class ElectricItem : MonoBehaviour
{
    public int Power { get; set; } = 0;                 // 消費電力
    public bool ChargeFlag { get; set; } = false;       // 電気を入れる/充電　OnOff可能
    public bool IsElectricEvent { get; set; } = false;  // Event中かどうか
    public bool IsChargeEvent { get; set; } = false;    // Event実行済かどうか　Onのみ
    public Image AnnounceImage { get; set; } = null;    // 指示表示画像

    public virtual void Event() { }

}
