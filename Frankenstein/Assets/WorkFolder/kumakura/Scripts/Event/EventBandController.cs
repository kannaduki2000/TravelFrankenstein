using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DebugLogUtility;

public class EventBandController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Image[] eventBands = new Image[2];
    private Vector3[] initialPosY = new Vector3[2];
    private Vector3[] eventBandPosY = new Vector3[2];
    private System.Action callback;

    void Start()
    {
        for (int i = 0; i < eventBandPosY.Length; i++)
        {
            // イベント中の帯の位置の記憶
            eventBandPosY[i] = eventBands[i].transform.position;
        }
        // 帯を画面外に動かす
        eventBands[0].transform.position = new Vector3(eventBands[0].transform.position.x, 
            eventBands[0].transform.position.y + eventBands[0].rectTransform.sizeDelta.y, 0);
        eventBands[1].transform.position = new Vector3(eventBands[1].transform.position.x, 
            eventBands[1].transform.position.y - eventBands[0].rectTransform.sizeDelta.y, 0);
        for (int i = 0; i < initialPosY.Length; i++)
        {
            // イベントを行っていない時の帯の位置の記憶
            initialPosY[i] = eventBands[i].transform.position;
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// イベントの開始時
    /// </summary>
    /// <param name="_callback">帯の表示後に行うイベント</param>
    public void EventStart(System.Action _callback = null)
    {
        callback = _callback;
        StartCoroutine(BandMove(true));
    }

    /// <summary>
    /// イベント終了時
    /// </summary>
    /// <param name="_callback">帯の表示終了後に行うイベント</param>
    public void EventEnd(System.Action _callback = null)
    {
        callback = _callback;
        StartCoroutine(BandMove(false));
    }

    private IEnumerator BandMove(bool _eventStart)
    {
        if (_eventStart)
        {
            while (eventBands[1].transform.position != eventBandPosY[1])
            {
                for (int i = 0; i < eventBands.Length; i++)
                {
                    eventBands[i].transform.position = Vector3.MoveTowards(eventBands[i].transform.position, eventBandPosY[i], moveSpeed * Time.deltaTime);
                }
                yield return null;
            }
        }
        else
        {
            while (eventBands[1].transform.position != initialPosY[1])
            {
                for (int i = 0; i < eventBands.Length; i++)
                {
                    eventBands[i].transform.position = Vector3.MoveTowards(eventBands[i].transform.position, initialPosY[i], moveSpeed * Time.deltaTime);
                }
                yield return null;
            }
        }
        if (callback != null) callback();
        yield break;
    }
}
