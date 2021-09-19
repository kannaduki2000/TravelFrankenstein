using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPush : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    private float speed = 2f;

    [SerializeField] GameObject gareki;

    public PhysicsMaterial2D pm;
    GameObject front;

    public bool crash = true;
    public bool garekiCrash = false;
    public bool rot = false;
    public bool carMove = false;
    [SerializeField] private EnemyController enemy;

    public PhysicMaterial material;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        pm = Resources.Load("yuka") as PhysicsMaterial2D;
        pm = GetComponent<PhysicsMaterial2D>();
    }

    void Update()
    {
        if (crash == true)
        {
            Crash();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //がれきに当たったら
        if (collision.gameObject.tag == "Gareki")
        {
            carMove = true;
            rigid2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            rigid2D.velocity = Vector3.zero;
            rigid2D.angularVelocity = 0;
            rot = true;
            gareki.SetActive(false);
            //時差発動「車破壊」
            Invoke("CarCrash", 2.0f);
        }
    }

    public void Crash()
    {
        if(!carMove)
        {
            GetComponent<Collider2D>().isTrigger = false;
            rigid2D.bodyType = RigidbodyType2D.Dynamic;
            Transform go = this.transform;
            Vector2 carposition = go.position;

            carposition.x = Mathf.MoveTowards(carposition.x, 5.5f, Time.deltaTime * speed);
            go.position = carposition;
        }
    }

    private void CarCrash()
    {
        // エネミーが歩けるようにする
        //enemy.EnemyMove();
        // 操作権を強制的にPlayerにする
        //enemy.isFollowing = false;
        //enemy.Follow = true;

        enemy.isFollowing = false;
        enemy.enemyMove = true;
        enemy.mt.player_Move = false;
        enemy.camera.GetComponent<CameraClamp>().targetToFollow = enemy.Player.transform;
        //車消す
        gameObject.SetActive(false);
    }
}