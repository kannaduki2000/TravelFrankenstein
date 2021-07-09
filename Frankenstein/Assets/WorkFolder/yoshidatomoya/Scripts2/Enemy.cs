using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    public Vector3 StartPos; // �J�n�ʒu
    public Vector3 EndPos; �@// �I���ʒu
    public float time;       // ���Ԏw��
    private Vector3 deltaPos;
    private float elapsedTime;
    private bool bStartToEnd = true;
    private float UpdateTimer = 0f;
    private float TimeRimit = 2.0f;
    private bool move = false;

    //private NavMeshAgent navMeshAgent;

    public Vector3 playerPos;
    public float moveSpeed = 1.0f;
    bool tossinFlag = false;


    private Rigidbody2D rd;
    // Start is called before the first frame update
    void Start()
    {

        //navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent��ێ����Ă���

        // StartPos�������ʒu�ɐݒ�
        transform.position = StartPos;
        // �P�b������̈ړ��ʂ��Z�o
        deltaPos = (EndPos - StartPos) / time;
        elapsedTime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (tossinFlag)
        {
            float speed = moveSpeed * Time.deltaTime;
            Vector2.MoveTowards(transform.position, playerPos, speed);
            if (this.transform.position == playerPos)
            {
                Debug.Log("loopEnd");
                tossinFlag = false;
            }
            else
            {
                Debug.Log("loop");
                return;
            }
        }
        Debug.Log("looooooooop");
        if(move == false)
        {
            UpdateTimer += Time.deltaTime;
        }
        if(UpdateTimer >= TimeRimit)
        {
            move = true;
            UpdateTimer = 0;
        }
        // �P�b������̈ړ��ʂ�Time.deltaTime���|�����1�t���[��������̈ړ��ʂƂȂ�
        if (move == true)
        {
            // Time.deltaTime�͑O��Update���Ă΂�Ă���̌o�ߎ���
            transform.position += deltaPos * Time.deltaTime;
            // ���H���H���]�p�o�ߎ���
            elapsedTime += Time.deltaTime;
        }
        // �ړ��J�n���Ă���̌o�ߎ��Ԃ�time�𒴂���Ɖ��H���H���]
        if(elapsedTime > time)
        {
            if(bStartToEnd)
            {
                
                // StartPos��EndPos�������̂Ŕ��]����EndPos��StartPos�ɂ���
                // ���݂̈ʒu��EndPos�Ȃ̂�StartPos - EndPos��EndPos��StartPos�̈ړ��ʂɂȂ�
                deltaPos = (StartPos - EndPos) / time;
                // �덷������Ƃ����\�������邽�ߔO�̂��߂ɃI�u�W�F�N�g�̈ʒu��EndPos�ɐݒ�
                transform.position = EndPos;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // ��̋t
                deltaPos = (EndPos - StartPos) / time;
                transform.position = StartPos;
                transform.localScale = new Vector3(1, 1, 1);

            }
            // ���H���H�̃t���O���]
            bStartToEnd = !bStartToEnd;
            // ���H���H���]�p�o�ߎ��ԃN���A
            elapsedTime = 0;
            move = false;
        }
    }




    // CollisionDetector.cs��onTriggerStay�ɃZ�b�g���A�Փ˒��Ɏ��s�����B
    public void OnDetectObject(Collider collider)
    {
        Debug.Log("start");
        // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        if (collider.CompareTag("Player"))
        {
            //playerPos = collider.transform.position;
            //enemy.StartCoroutine(Tossin());
            //navMeshAgent.destination = collider.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !tossinFlag)
        {
            tossinFlag = true;
            playerPos = collision.transform.position;
            //StartCoroutine(Tossin());
        }

    }

    //public IEnumerator Tossin()
    //{
    //    tossinFlag = true;
    //    Debug.Log("PlayerPos : " + playerPos);
    //    while (this.transform.position != playerPos)
    //    {
    //        Debug.Log("bbb");
    //        Vector2.MoveTowards(transform.position, playerPos, moveSpeed);
    //        yield return null;
    //    }
    //    tossinFlag = false;
    //    yield break;
    //}

    // �g���������ł���֗��Ȋ֐�
    //public override void Following()
    //{
    //    int a = 0;
    //}

}

// �G�l�~�[���g�̃e���g���[�Ԃ��s��������Z
// �ҋ@���Ԃ���i2�b�ԁj�Z
// �t�����P�����߂Â��Ă�����ːi����
// �t�����P���̎����@�m�̓G�l�~�[�𒆐S�ɂ���i�G�l�~�[���������王���@�m���ꏏ�ɓ����j�����Z
// �ːi����������S�b�ԑҋ@�@���̂��Ǝ����̃e���g���[�ɖ߂�
// �S�b�ԑҋ@��A�����@�m���Ƀt�����P����������t�����P���ɍĂѓːi
// �ːi�����ꏊ�Ƀt�����P�������Ȃ�������Q�b�ԑҋ@���Ď����̃e���g���[�ɖ߂�
// 
