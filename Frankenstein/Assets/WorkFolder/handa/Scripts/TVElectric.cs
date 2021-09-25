using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVElectric : ElectricItem
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;

    [SerializeField] private int power;

    Animator TVAnim;

    // Start is called before the first frame update
    void Start()
    {
        Power = power;
        ChargeFlag = true;
        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.S1_SquareButton_Absorption);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            announceObject.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            announceObject.enabled = false;
        }
    }
}
