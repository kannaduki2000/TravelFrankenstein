using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�J�����p�A�����C���ŏ���������
//���ۂ͗��p���Ȃ���I�g��Ȃ��łˁI
[RequireComponent(typeof(Camera))]

public class CameraTest : MonoBehaviour
{
    public EnemyFollowing enemyfollowing;
    public MinecartPush mpush;
    public PushButton pushb;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject MineCart;

    //������̋ɂ݉���
    void Update()
    {
        Vector2 playerPos = Player.transform.position;
        transform.position = new Vector3(playerPos.x, 0, -10);

        if(enemyfollowing.enemyMove == false)
        {
            Vector2 enemyPos = Enemy.transform.position;
            transform.position = new Vector3(enemyPos.x, 0, -10);
        }

        if (mpush.minecartpush == true && pushb.notpushingbutton == false)
        {
            Vector2 minecartPos = MineCart.transform.position;
            transform.position = new Vector3(minecartPos.x, 0, -10);
        }
    }
}
