using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableCarController : MonoBehaviour
{
    [SerializeField] private Renderer eve;
    [SerializeField] private Renderer[] cableCars;
    [SerializeField] private GameObject[] colliders;
    [SerializeField, Header("��~�n�_")] private GameObject cableCarStopPosition;   // ��~�n�_
    [SerializeField] private float moveSpeed = 5.0f;
    private bool loopFlag = false;

    private bool trigger = false;

    void Start()
    {
        // �ŏ��͓����蔻�肪�Ȃ��̂ŏ���
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
    /// �P�[�u���J�[�����̒n�_�܂ňړ�������
    /// </summary>
    public void CableCarMove()
    {
        if (loopFlag) { return; }
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
        EventFlagManager.Instance.SetFlagState(EventFlagName.cableCarStop, true);
        yield break;
    }

    /// <summary>
    /// �P�[�u���J�[�̕\��/��\��
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
