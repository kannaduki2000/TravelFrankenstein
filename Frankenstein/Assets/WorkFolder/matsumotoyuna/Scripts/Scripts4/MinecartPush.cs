using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Physics;

//�G�l�~�[���g���b�R�̑O��R�������ƁA
//�g���b�R�������d�g�݂̃X�N���v�g����I
public class MinecartPush : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    private float speed = 2f;
    public bool minecartpush = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floar")
        {
            //����̏��ɒ����܂ł�Z��]���Œ肵�Ă܂�
            //���̏���Floar�^�O��ݒ肵�Ă��ꂽ�܂�
            rigid2D.constraints = RigidbodyConstraints2D.None;
            //int player = LayerMask.NameToLayer("Player");
            //int enemy = LayerMask.NameToLayer("Enemy");
            //Physics.IgnoreLayerCollision(player, enemy);
            //gameObject.layer = LayerName.MineCart;
        }
    }

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (minecartpush == true)
        {
            MineCartPush();
        }
    }

    public void MineCartPush()
    {
        Transform push = this.transform;
        Vector2 minecartposition = push.position;

        minecartposition.x = Mathf.MoveTowards(minecartposition.x, 3.5f, Time.deltaTime * speed);
        push.position = minecartposition;
    }
}
