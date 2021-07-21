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

    public PhysicMaterial material;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        pm = Resources.Load("yuka") as PhysicsMaterial2D;
        pm = GetComponent<PhysicsMaterial2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            crash = true;
        }
        if (crash == true)
        {
            Crash();
        }

        if(rot == true)
        {
            transform.Rotate(0f, 0f, -this.speed / 6);
        }

        if (rot == true && transform.rotation.z >= 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            rot = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ç™ÇÍÇ´Ç…ìñÇΩÇ¡ÇΩÇÁ
        if (collision.gameObject.tag == "Gareki")
        {
            //rigid2D.sharedMaterial = pm;
            //pm.friction = 10000000;
            //pm.dynamicFriction = 500000000000000000;
            //pm.staticFriction = 1;
            rigid2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            rigid2D.velocity = Vector3.zero;
            rigid2D.angularVelocity = 0;
            rot = true;
            //rigid2D.velocity = Vector2.zero;
            //Destroy(gareki);
            gareki.SetActive(false);
            //éûç∑î≠ìÆÅué‘îjâÛÅv
            Invoke("CarCrash", 2.0f);
        }
    }

    private void Crash()
    {
        GetComponent<Collider2D>().isTrigger = false;
        rigid2D.bodyType = RigidbodyType2D.Dynamic;
        Transform go = this.transform;
        Vector2 carposition = go.position;

        carposition.x = Mathf.MoveTowards(carposition.x, 5.5f, Time.deltaTime * speed);
        go.position = carposition;
    }

    private void CarCrash()
    {
        //é‘è¡Ç∑
        gameObject.SetActive(false);
    }
}