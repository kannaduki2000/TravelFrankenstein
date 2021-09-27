using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DebugLogUtility;

public class EveController : MonoBehaviour
{
    private bool endingTrigger = false;
    private bool endFlagTrigger = false;
    [HideInInspector] public Animator anim;
    [SerializeField] private Animator playerAnim;
    private FadeControl fade;
    private EventBandController eventBandCon;
    private PlayerController player;

    private float time = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        fade = FindObjectOfType<FadeControl>();
        eventBandCon = FindObjectOfType<EventBandController>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // デバッグ用
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    EndingManager.Instance.Ending();
        //}
    }

    public void GetUpPlayerAnim()
    {
        playerAnim.SetTrigger("EndGetUp");
    }

    /// <summary>
    /// BadEnd
    /// </summary>
    public void FadeOut()
    {
        if (endFlagTrigger) return;
        // プレイヤーのHPが20未満ならフェードアウト

        if (FindObjectOfType<PlayerController>().HP < 20)
        {
            endFlagTrigger = true;
            fade = FindObjectOfType<FadeControl>();
            fade.Fade("out", () =>
            {
                eventBandCon.InitEventBand();
                fade.sceneChange.SceneSwitching("BadEndScene");
            });
        }
    }

    private IEnumerator Delay()
    {
        while (time < 2)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        EndingManager.Instance.Ending();
        yield break;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !endingTrigger)
        {
            endingTrigger = true;
            player.PlayerNotMove();
            eventBandCon.EventStart(() =>
            {
                StartCoroutine(Delay());
                //EndingManager.Instance.Ending();
            });
        }
    }
}
