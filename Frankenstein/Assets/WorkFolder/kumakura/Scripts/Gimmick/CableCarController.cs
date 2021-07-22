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
    [SerializeField] private Collider2D rubble;
    private PlayerController playerCon;
    private bool loopFlag = false;

    private bool trigger = false;

    void Start()
    {
        playerCon = FindObjectOfType<PlayerController>();
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
            Debug.Log("CableCar");
            // ��x�ł��Ă΂ꂽ���x�ڂ͌Ă΂�Ȃ��悤�ɂ��鏈��
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
        // Player��ҋ@���[�V�����ɕύX
        playerCon.anim.SetBool("Walking", false);
        // Player�̓������~�߂�
        playerCon.PlayerNotMove();
        // ���x�������I��0�ɂ���
        //playerCon.rb2d.velocity = Vector2.zero;
        // 
        //playerCon.vx = 0;
        ViewCableCar(true);
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        loopFlag = true;
        while (gameObject.transform.position != cableCarStopPosition.transform.position)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                cableCarStopPosition.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("MoveEnd");
        loopFlag = false;
        // �P�[�u���J�[�̓����蔻���Active�ɂ���
        ColliderActive();
        // �C���̔�\��
        eve.enabled = false;
        // �K���L�̓����蔻��̏���
        rubble.enabled = false;
        playerCon.PlayerMove();
        EventFlagManager.Instance.SetFlagState(EventFlagName.cableCarStop, true);
        yield break;
    }

    /// <summary>
    /// �P�[�u���J�[�̕\��/��\��
    /// </summary>
    /// <param name="isEnabled">enable</param>
    public void ViewCableCar(bool isEnabled)
    {
        eve.enabled = isEnabled;
        for (int i = 0; i < cableCars.Length; i++)
        {
            cableCars[i].enabled = isEnabled;
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
