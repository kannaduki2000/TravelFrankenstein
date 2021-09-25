using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLight : ElectricItem
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;

    // Start is called before the first frame update
    void Start()
    {
        Power = 30;
        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.S1_SquareButton_Input);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsChargeEvent)
        {
            announceObject.enabled = false;

            EventFlagManager.Instance.SetFlagState(EventFlagName.cableCarStart, true);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
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
