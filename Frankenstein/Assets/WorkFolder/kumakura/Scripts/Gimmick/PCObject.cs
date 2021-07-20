using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCObject : ElectricItem
{
    [SerializeField] private int power;
    [SerializeField] private Image announceImage;
    [SerializeField] private Sprite eveReport;
    [SerializeField] private Image image;
    [SerializeField] private PlayerController player;


    void Start()
    {
        Power = power;
        AnnounceImage = announceImage;
    }

    void Update()
    {
        if (IsChargeEvent)
        {
            AnnounceImage.enabled = false;
        }
    }

    public override void Event()
    {
        IsElectricEvent = true;
        player.PlayerNotMove();
        // イヴのレポートの表示
        image.enabled = true;
        image.sprite = eveReport;
    }

    public void EveReportUnEnabled()
    {
        image.enabled = false;
        player.textCon.SetTextActive(true, ()=> player.PlayerMove());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AnnounceImage.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AnnounceImage.enabled = false;
        }
    }
}
