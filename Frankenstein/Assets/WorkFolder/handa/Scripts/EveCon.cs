using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EveCon : ElectricItem
{
    public Animator anim;
    [SerializeField] private PlayerController player;
    [SerializeField] private Sprite announceImage;
    [SerializeField] private ImageData imageData;
    [SerializeField] private Image announceObject;

    public bool endFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        imageData = FindObjectOfType<ImageData>();
        announceImage = imageData.GetAnnounceImage(AnnounceName.S1_SquareButton_Input);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsChargeEvent)
        {
            announceObject.enabled = false;
        }
        
    }

    public override void Event()
    {
        player.PlayerNotMove();

        if (EventFlagManager.Instance.GetFlagState(EventFlagName.trueEnd))
        {
            anim.SetBool("Electric", true);
        }
        else if (EventFlagManager.Instance.GetFlagState(EventFlagName.badEnd))
        {
            anim.SetBool("Electric", true);
        }
    }

    public void ARoot()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            announceObject.enabled = true;

            endFlag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            announceObject.enabled = false;

            endFlag = true;
        }
    }

}
