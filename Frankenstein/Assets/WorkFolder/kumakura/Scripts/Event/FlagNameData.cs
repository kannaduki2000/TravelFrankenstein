
[System.Serializable]
public enum EventFlagName
{
    // Tutorial
    frankensteinGetUp,  // フランケンが起き上がった後　Textの表示
    getupFlag,          // アニメーション用　一度でも起き上がったかどうか
    text_System,        // 最初のテキスト表示後　移動可能になる
    text_SystemEnd,     // 移動可能になった後に移動する　Textの非表示
    eveReport,          // レポート表示終了後　Textの表示
    text_Eve,           // イヴのテキスト表示終了後　電気の吸収が可能になる　もしかしたらいらない
    electricAabsorption,// 電気を吸収した後　エネミーに電気を入れる事が可能になる                      
                        // ここに投げる関係のフラグが追加されるかも

    isFade,             // 


    // Stage1
    stage1_Title,       // ステージ１のタイトルが表示された後　フランケンの操作が可能になる
    stage1_ControlPanel,// 操作盤に電気を入れた後　床が出てきて崖の当たり判定の消滅
    cableCarStart,      // 踏切に電気を入れた後　ケーブルカーのイベントの実行
    cableCarStop,       // ケーブルカーが停車した後　フランケンが移動可能になる
    enemyEscape,        // cableCarStopがtrueの時にエネミーが倒された時　他のエネミーが逃げ出す
    enemyElectricCharge,// 最初のエネミー追従の時　ギアハーツの生体確認UIの表示
    enemyReport,        // ギアハーツの生体確認終了後　操作切り替えが可能になる
    pushCar,            // 車を押すを押した後　操作権をフランケンに強制的に変更、エネミーを呼ぶ表示　カメラの移動がしたい
    shoppingMall,       // ショッピングモールに近付いたら　テキストの表示
    stage1_Gear,        // 歯車になるを押した後　ショッピングモールの扉が開く

    // End
    FlagEnd,            // フラグリストの最後を意味するので消さない＆位置ずらさないで
}
