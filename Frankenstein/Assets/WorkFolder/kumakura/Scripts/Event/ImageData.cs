using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AnnounceName
{
    // Tutorial
    T_Put_Electric_Enemy,           // エネミーの死体に電気を入れる
    T_Throw_Having,                 // 投げる、持つ
    T_Come_Out_Home,                // 家から出る
    T_Release_Object,               // 長押しで上に投げる、離す
    T_SquareButton,                 // 四角ボタン
    T_CircleButton_StartUp,         // 丸ボタン：起動する
    T_Leftstick_Move,               // 左スティック：移動
    T_SquareButton_Absorption,      // 四角ボタン：電気を吸収する
    T_SquareButton_Input,           // 四角ボタン：電気を入れる

    // Stage1
    S1_TriangleButton_Gear,         // 三角ボタン：歯車になる
    S1_CircleButton_EnemyCall,      // 丸ボタン：エネミーを呼ぶ
    S1_SquareButton_ElectricCable,  // 四角ボタン：電線を伝う
    S1_LButton_EnemyChange,         // Lボタン：操作切り替え
    S1_RButton_Push,                // Rボタン：押す
    S1_SquareButton,                // 四角ボタン
    S1_SquareButton_Input,          // 四角ボタン：電気を入れる
    S1_SquareButton_Absorption,     // 四角ボタン：電気を吸収する

}

[System.Serializable]
public enum ReportName
{
    eveReport,          // イヴレポート
    gearHertsReport,    // ギアハーツレポート

}

public class ImageData : MonoBehaviour
{
    public Sprite[] AnnounceImageArray;
    public Sprite[] ReportImageArray;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 指示表示の画像をセット
    /// </summary>
    /// <param name="annouceSprite">セットするObject</param>
    /// <param name="announceName">セットしたい画像の名前</param>
    public Sprite GetAnnounceImage(AnnounceName announceName)
    {
        return AnnounceImageArray[(int)announceName];
    }

    public Sprite GetReportImage(ReportName reportName)
    {
        return ReportImageArray[(int)reportName];
    }
}
