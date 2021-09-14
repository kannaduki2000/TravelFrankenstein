using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnnounceDisplayCollider : MonoBehaviour
{

    [SerializeField] private Image announceImage;
    [SerializeField] private Sprite[] displayAnnounceSprite;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (EventFlagManager.Instance.GetFlagState(EventFlagName.enemyCharge))
            {
                announceImage.sprite = displayAnnounceSprite[0];
            }
        }
    }
}
