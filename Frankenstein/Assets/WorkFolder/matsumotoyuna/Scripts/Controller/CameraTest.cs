using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラ用、私が気分で書いただけ
//実際は利用しないよ！使わないでね！
[RequireComponent(typeof(Camera))]

public class CameraTest : MonoBehaviour
{
    public EnemyFollowing enemyfollowing;
    public MinecartPush mpush;
    public PushButton pushb;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject MineCart;

    //非効率の極み乙女
    void Update()
    {
        Vector2 playerPos = Player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, -10);;

        if(enemyfollowing.enemyMove == false)
        {
            Vector2 enemyPos = Enemy.transform.position;
            transform.position = new Vector3(enemyPos.x, playerPos.y, -10);
        }

        if (mpush.minecartpush == true && pushb.notpushingbutton == false)
        {
            Vector2 minecartPos = MineCart.transform.position;
            transform.position = new Vector3(minecartPos.x, playerPos.y, -10);
        }
    }
}
