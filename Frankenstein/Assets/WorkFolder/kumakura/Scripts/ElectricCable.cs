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
        // �d���̌o�ߒn�_�̐����擾
        pointArray = new GameObject[gameObject.transform.childCount];
        // �d���̌o�ߒn�_�̉񐔕��J��Ԃ�������
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            // �e�o�ߒn�_�̏���z��Ɋi�[
            pointArray[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        
    }

    /// <summary>
    /// �d���ɐG�ꂽ�I�u�W�F�N�g���ړ������鏈��
    /// </summary>
    /// <param name="_moveObject">�ړ����������I�u�W�F�N�g</param>
    /// <param name="_startPoint">true:StartCable / false:EndCable</param>
    public void CablePointMove(GameObject _moveObject, bool _startPoint = true)
    {
        // Update�ŌĂ΂�Ă����v�Ȃ悤�Ƀt���O�Ǘ�
        if (loopFlag) { return; }
        StartCoroutine(MoveLoop(_moveObject, _startPoint));
    }


    /// <summary>
    /// �񓯊��Ŋe�d�������񂷂鏈��
    /// </summary>
    /// <param name="_loopObject">�ړ�������I�u�W�F�N�g</param>
    /// <param name="_startPoint">true:StartCable / false:EndCable</param>
    /// <returns></returns>
    private IEnumerator MoveLoop(GameObject _loopObject, bool _startPoint)
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
            for (int i = 0; i < pointArray.Length; i++)
            {
                // ����point�ƍ��W�������ɂȂ�܂ŌJ��Ԃ�
                while (_loopObject.transform.position != pointArray[i].transform.position)
                {
                    // ����point�̍��W�Ɍ���������
                    _loopObject.transform.position = Vector2.MoveTowards(_loopObject.transform.position, pointArray[i].transform.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
        else
        {
            // point�̔z��̐������J��Ԃ�
            for (int i = pointArray.Length - 1; i >= 0; i--)
            {
                // ����point�ƍ��W�������ɂȂ�܂ŌJ��Ԃ�
                while (_loopObject.transform.position != pointArray[i].transform.position)
                {
                    // ����point�̍��W�Ɍ���������
                    _loopObject.transform.position = Vector2.MoveTowards(_loopObject.transform.position, pointArray[i].transform.position, moveSpeed * Time.deltaTime);
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
