using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCable : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    private GameObject[] pointArray;
    private bool loopFlag = false;


    void Start()
    {
        // 電線の経過地点の数を取得
        pointArray = new GameObject[gameObject.transform.childCount];
        // 電線の経過地点の回数分繰り返し文を回す
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            // 各経過地点の情報を配列に格納
            pointArray[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 電線に触れたオブジェクトを移動させる処理
    /// </summary>
    /// <param name="_moveObject">移動させたいオブジェクト</param>
    /// <param name="_startPoint">true:StartCable / false:EndCable</param>
    public void CablePointMove(GameObject _moveObject, bool _startPoint = true)
    {
        // Updateで呼ばれても大丈夫なようにフラグ管理
        if (loopFlag) { return; }
        StartCoroutine(MoveLoop(_moveObject, _startPoint));
    }


    /// <summary>
    /// 非同期で各電線を周回する処理
    /// </summary>
    /// <param name="_loopObject">移動させるオブジェクト</param>
    /// <param name="_startPoint">true:StartCable / false:EndCable</param>
    /// <returns></returns>
    private IEnumerator MoveLoop(GameObject _loopObject, bool _startPoint)
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
            for (int i = 0; i < pointArray.Length; i++)
            {
                // 次のpointと座標が同じになるまで繰り返す
                while (_loopObject.transform.position != pointArray[i].transform.position)
                {
                    // 次のpointの座標に向かう処理
                    _loopObject.transform.position = Vector2.MoveTowards(_loopObject.transform.position, pointArray[i].transform.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
        else
        {
            // pointの配列の数だけ繰り返す
            for (int i = pointArray.Length - 1; i >= 0; i--)
            {
                // 次のpointと座標が同じになるまで繰り返す
                while (_loopObject.transform.position != pointArray[i].transform.position)
                {
                    // 次のpointの座標に向かう処理
                    _loopObject.transform.position = Vector2.MoveTowards(_loopObject.transform.position, pointArray[i].transform.position, moveSpeed * Time.deltaTime);
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
