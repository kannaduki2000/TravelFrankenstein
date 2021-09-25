using DualShockInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEnemy : MonoBehaviour
{
    [SerializeField] GameObject GiyaH;
    [SerializeField] GameObject GiyaH2;
    [SerializeField] GameObject Giya1;

    [SerializeField] private RotLadder rot;
    [SerializeField] private EnemyController eneCon, eneCon2;

    private float speed = 0;
    private bool kasokuuuuuu = false;
    public bool kasokudekiru = false;
    public bool rothaguruma = false;
    public bool GiyaOnTrigger = true;

    public bool enemy1Flag = false;
    public bool enemy2Flag = false;


    [SerializeField] GameObject Gate;
    private float upSpeed = 5f;

    private void Start()
    {
        Gate = GameObject.Find("Ladder");
    }

    void Update()
    {
        if (GiyaOnTrigger == true)
        {
            GiyaChange();
        }

        //動くか否か
        if (rothaguruma == false && kasokudekiru == false)
        {
            kasokuuuuuu = false;
        }
        
        //若干加速するよ
        if(rothaguruma == true && kasokudekiru == false)
        {
            this.speed = 0.001f;
            kasokuuuuuu = true;
            kasokudekiru = true;
        }

        //回転スピード
        if(kasokuuuuuu == true)
        {
            transform.Rotate(0, 0, this.speed);
        }

        if (speed <= 0.1)
        {
            this.speed += 0.002f;
        }
    }

    public void GiyaChange()
    {
        GiyaH.SetActive(false);
        Giya1.SetActive(true);
        rothaguruma = true;
        Transform gate = Gate.transform;
        Vector2 vector2 = gate.position;
        vector2.y = Mathf.MoveTowards(vector2.y, 2, Time.deltaTime * upSpeed);
        gate.position = vector2;
        eneCon.isFollowing = false;
        eneCon.enemyMove = true;
        eneCon.mt.player_Move = false;
        Invoke("GaetUp", 1.0f);

        //if (enemy1Flag == true)
        //{
           
        //}
        //else if(enemy2Flag == true)
        //{
        //    GiyaH2.SetActive(false);
        //    eneCon2.isFollowing = false;
        //    eneCon2.enemyMove = true;
        //    eneCon2.mt.player_Move = false;
        //    eneCon2.camera.GetComponent<CameraClamp>().targetToFollow = eneCon.Player.transform;
        //}
       
    }

    public void GaetUp()
    {
        eneCon.camera.GetComponent<CameraClamp>().targetToFollow = eneCon.Player.transform;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        enemy1Flag = true;
    //    }
        
    //    if(collision.gameObject.tag == "Enemy2")
    //    {
    //        enemy2Flag = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        enemy1Flag = false;
    //    }

    //    if (collision.gameObject.tag == "Enemy2")
    //    {
    //        enemy2Flag = false;
    //    }
    //}
}