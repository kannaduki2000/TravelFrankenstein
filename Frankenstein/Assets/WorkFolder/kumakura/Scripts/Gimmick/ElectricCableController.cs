using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCableController : MonoBehaviour
{
    [Header("移動出来る電柱の数だけ配列を増やしてアタッチしてください")]
    [Header("アタッチした順番にIDが割り振られます")]
    // 電線のオブジェクトをここにアタッチ
    public ElectricCableArray[] electricCableArray;

    [SerializeField] private float moveSpeed = 10f;
    private bool loopFlag = false;

    /// <summary>
    /// 電線に触れたオブジェクトを移動させる処理
    /// </summary>
    /// <param name="_moveObject">移動させたいオブジェクト</param>
    /// <param name="_id">どこの電柱を渡るか、電柱のID（移動出来る電柱を左側から0,1,2...として数える）</param>
    /// <param name="_startPoint">true:StartCable / false:EndCable</param>
    public void CablePointMove(GameObject _moveObject, int _id,  bool _startPoint = true)
    {
        // Updateで呼ばれても大丈夫なようにフラグ管理
        if (loopFlag) { return; }
        StartCoroutine(MoveLoop(_moveObject, _id, _startPoint));
    }


    /// <summary>
    /// 非同期で各電線を周回する処理
    /// </summary>
    /// <param name="_loopObject">移動させるオブジェクト</param>
    /// /// <param name="_id">どこの電柱を渡るか、電柱のID（移動出来る電柱を左側から0,1,2...として数える）</param>
    /// <param name="_startPoint">true:StartCable / false:EndCable</param>
    /// <returns></returns>
    private IEnumerator MoveLoop(GameObject _loopObject, int _id, bool _startPoint)
    {
        // ループの開始
        loopFlag = true;
        // RigitBody2Dの取得
        var body = _loopObject.GetComponent<Rigidbody2D>();
        // 現在のBodyTypeを取得
        var currentBodyType = body.bodyType;
        // BodtTypeをstaticに変更
        body.bodyType = RigidbodyType2D.Static;
        // 開始地点の判別
        if (_startPoint)
        {
            // pointの配列の数だけ繰り返す
            for (int i = 0; i < electricCableArray[_id].pointArray.Length; i++)
            {
                // 次のpointと座標が同じになるまで繰り返す
                while (_loopObject.transform.position != electricCableArray[_id].pointArray[i].transform.position)
                {
                    // 次のpointの座標に向かう処理
                    _loopObject.transform.position = Vector2.MoveTowards(_loopObject.transform.position, electricCableArray[_id].pointArray[i].transform.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
        else
        {
            // pointの配列の数だけ繰り返す
            for (int i = electricCableArray[_id].pointArray.Length - 1; i >= 0; i--)
            {
                // 次のpointと座標が同じになるまで繰り返す
                while (_loopObject.transform.position != electricCableArray[_id].pointArray[i].transform.position)
                {
                    // 次のpointの座標に向かう処理
                    _loopObject.transform.position = Vector2.MoveTowards(_loopObject.transform.position, electricCableArray[_id].pointArray[i].transform.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
        // ループの終了
        loopFlag = false;
        // BodyTypeを元の変数に戻す
        body.bodyType = currentBodyType;
        yield break;
    }
}
