using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiyaGate : MonoBehaviour
{
    [SerializeField] GameObject GiyaH;
    [SerializeField] GameObject Giya1;

    [SerializeField] private EnemyController eneCon;
    private float speed = 0;
    private bool kasokuuuuuu = false;
    public bool kasokudekiru = false;
    public bool rothaguruma = false;
    public bool GateOnTrigger = false;

    [SerializeField] GameObject Gate;
    private float upSpeed = 5f;

    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;


    // Start is called before the first frame update
    void Start()
    {
        Gate = GameObject.Find("Gate");

        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.S1_TriangleButton_Gear);

    }

    // Update is called once per frame
    void Update()
    {
        if (GateOnTrigger == true)
        {
            GiyaChange();
            announceObject.enabled = false;
        }

        if (rothaguruma == false && kasokudekiru == false)
        {
            kasokuuuuuu = false;
        }

        if (rothaguruma == true && kasokudekiru == false)
        {
            this.speed = 0.001f;
            kasokuuuuuu = true;
            kasokudekiru = true;
        }

        if (kasokuuuuuu == true)
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
        vector2.y = Mathf.MoveTowards(vector2.y, -14, Time.deltaTime * upSpeed);
        gate.position = vector2;
        eneCon.isFollowing = false;
        eneCon.enemyMove = true;
        eneCon.mt.player_Move = false;
        Invoke("GaetUp", 2.0f);
    }

    public void GaetUp()
    {
        eneCon.camera.GetComponent<CameraClamp>().targetToFollow = eneCon.Player.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!EventFlagManager.Instance.GetFlagState(EventFlagName.pushCar)) { return; }
            announceObject.enabled = true;
            Debug.Log("in");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!EventFlagManager.Instance.GetFlagState(EventFlagName.pushCar)) { return; }
        if (collision.gameObject.tag == "Enemy")
        {
            announceObject.enabled = false; 
            Debug.Log("out");

        }
    }

}
