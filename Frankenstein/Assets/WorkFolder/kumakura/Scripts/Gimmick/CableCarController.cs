using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableCarController : MonoBehaviour
{
    [SerializeField] private Renderer eve;
    [SerializeField] private Renderer[] cableCars;
    [SerializeField] private GameObject[] colliders;
    [SerializeField, Header("停止地点")] private GameObject cableCarStopPosition;   // 停止地点
    [SerializeField] private float moveSpeed = 5.0f;
    private PlayerController playerCon;
    private bool loopFlag = false;

    private bool trigger = false;

    void Start()
    {
        playerCon = FindObjectOfType<PlayerController>();
        // 最初は当たり判定がないので消す
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].gameObject.SetActive(false);
        }
        ViewCableCar(false);
    }

    void Update()
    {
        if (trigger) { return; }
        if (EventFlagManager.Instance.GetFlagState(EventFlagName.cableCarStart))
        {
            trigger = true;
            CableCarMove();
        }
    }

    /// <summary>
    /// ケーブルカーを特定の地点まで移動させる
    /// </summary>
    public void CableCarMove()
    {
        if (loopFlag) { return; }
        // Playerの動きを止める
        playerCon.player_Move = true;
        // 速度を強制的に0にする
        // PlayerControllerのRigitbody2Dがpublicになるまでコメント化しといても大丈夫
        playerCon.rb2d.velocity = Vector2.zero;
        playerCon.vx = 0;
        ViewCableCar(true);
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        loopFlag = true;
        while (gameObject.transform.position != cableCarStopPosition.transform.position)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, cableCarStopPosition.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        loopFlag = false;
        ColliderActive();
        eve.enabled = false;
        playerCon.player_Move = false;
        EventFlagManager.Instance.SetFlagState(EventFlagName.cableCarStop, true);
        yield break;
    }

    /// <summary>
    /// ケーブルカーの表示/非表示
    /// </summary>
    /// <param name="enabled">enable</param>
    public void ViewCableCar(bool enabled)
    {
        eve.enabled = enabled;
        for (int i = 0; i < cableCars.Length; i++)
        {
            cableCars[i].enabled = enabled;
        }
    }

    private void ColliderActive()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].gameObject.SetActive(true);
        }
    }
}
