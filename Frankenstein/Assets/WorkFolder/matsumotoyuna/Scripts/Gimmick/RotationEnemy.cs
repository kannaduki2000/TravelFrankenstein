using DualShockInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationEnemy : MonoBehaviour
{
    [SerializeField] GameObject GiyaH;
    [SerializeField] GameObject Giya1;

    [SerializeField] private RotLadder rot;
    [SerializeField] private EnemyController eneCon;

    private float speed = 0;
    private bool kasokuuuuuu = false;
    public bool kasokudekiru = false;
    public bool rothaguruma = false;
    public bool GiyaOnTrigger = true;

    private bool trigger = true;

    [SerializeField] GameObject Gate;
    private float upSpeed = 5f;


    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;

    private void Start()
    {
        Gate = GameObject.Find("Ladder");
        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.S1_TriangleButton_Gear);
    }

    void Update()
    {
        if (GiyaOnTrigger == true)
        {
            GiyaChange();
            announceObject.enabled = false;

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
        if (trigger)
        {
            eneCon.mt.player_Move = false;
            trigger = false;
        }
        eneCon.enemyMove = true;
        Invoke("GaetUp", 1.0f);
       
    }

    public void GaetUp()
    {
        eneCon.camera.GetComponent<CameraClamp>().targetToFollow = eneCon.Player.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            announceObject.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            announceObject.enabled = false;
        }
    }
}