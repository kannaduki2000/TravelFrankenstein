using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum CablePoint
{
    start,
    end,
}
public class CableData : MonoBehaviour
{
    [SerializeField] private ElectricCableArray cableArray;
    public CablePoint point; // 開始地点
    public int CableNum;     // 電線の番号

    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;

    void Start()
    {
        CableNum = cableArray.cableNum;

        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.S1_SquareButton_ElectricCable);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            announceObject.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            announceObject.enabled = false;
        }
    }
}
