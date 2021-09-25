using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarPushImage : MonoBehaviour
{
    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;
    [SerializeField] private CarPush car;


    // Start is called before the first frame update
    void Start()
    {
        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.S1_RButton_Push);
    }

    // Update is called once per frame
    void Update()
    {
        if(car.crash == true)
        {
            announceObject.enabled = false;
        }
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
