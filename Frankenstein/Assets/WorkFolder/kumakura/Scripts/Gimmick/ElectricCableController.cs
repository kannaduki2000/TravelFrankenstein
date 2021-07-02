using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCableController : MonoBehaviour
{
    [Header("�ړ��o����d���̐������z��𑝂₵�ăA�^�b�`���Ă�������")]
    [Header("�A�^�b�`�������Ԃ�ID������U���܂�")]
    // �d���̃I�u�W�F�N�g�������ɃA�^�b�`
    public ElectricCableArray[] electricCableArray;

    [SerializeField] private float moveSpeed = 10f;
    private bool loopFlag = false;

    /// <summary>
    /// �d���ɐG�ꂽ�I�u�W�F�N�g���ړ������鏈��
    /// </summary>
    /// <param name="_moveObject">�ړ����������I�u�W�F�N�g</param>
    /// <param name="_id">�ǂ��̓d����n�邩�A�d����ID�i�ړ��o����d������������0,1,2...�Ƃ��Đ�����j</param>
    /// <param name="_startPoint">true:StartCable / false:EndCable</param>
    public void CablePointMove(GameObject _moveObject, int _id,  bool _startPoint = true)
    {
        // Update�ŌĂ΂�Ă����v�Ȃ悤�Ƀt���O�Ǘ�
        if (loopFlag) { return; }
        StartCoroutine(MoveLoop(_moveObject, _id, _startPoint));
    }


    /// <summary>
    /// �񓯊��Ŋe�d�������񂷂鏈��
    /// </summary>
    /// <param name="_loopObject">�ړ�������I�u�W�F�N�g</param>
    /// /// <param name="_id">�ǂ��̓d����n�邩�A�d����ID�i�ړ��o����d������������0,1,2...�Ƃ��Đ�����j</param>
    /// <param name="_startPoint">true:StartCable / false:EndCable</param>
    /// <returns></returns>
    private IEnumerator MoveLoop(GameObject _loopObject, int _id, bool _startPoint)
    {
        // ���[�v�̊J�n
        loopFlag = true;
        // RigitBody2D�̎擾
        var body = _loopObject.GetComponent<Rigidbody2D>();
        // ���݂�BodyType���擾
        var currentBodyType = body.bodyType;
        // BodtType��static�ɕύX
        body.bodyType = RigidbodyType2D.Static;
        // �J�n�n�_�̔���
        if (_startPoint)
        {
            // point�̔z��̐������J��Ԃ�
            for (int i = 0; i < electricCableArray[_id].pointArray.Length; i++)
            {
                // ����point�ƍ��W�������ɂȂ�܂ŌJ��Ԃ�
                while (_loopObject.transform.position != electricCableArray[_id].pointArray[i].transform.position)
                {
                    // ����point�̍��W�Ɍ���������
                    _loopObject.transform.position = Vector2.MoveTowards(_loopObject.transform.position, electricCableArray[_id].pointArray[i].transform.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
        else
        {
            // point�̔z��̐������J��Ԃ�
            for (int i = electricCableArray[_id].pointArray.Length - 1; i >= 0; i--)
            {
                // ����point�ƍ��W�������ɂȂ�܂ŌJ��Ԃ�
                while (_loopObject.transform.position != electricCableArray[_id].pointArray[i].transform.position)
                {
                    // ����point�̍��W�Ɍ���������
                    _loopObject.transform.position = Vector2.MoveTowards(_loopObject.transform.position, electricCableArray[_id].pointArray[i].transform.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
        // ���[�v�̏I��
        loopFlag = false;
        // BodyType�����̕ϐ��ɖ߂�
        body.bodyType = currentBodyType;
        yield break;
    }
}
