using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DebugLogUtility;

public class EndingAnimationContol : MonoBehaviour
{
    private PlayerController player;
    private EveController eve;


    // デバッグ用
    public Animator playerAnim;
    public Animator eveAnim;


    // アニメーション用変数
    private string startEnding = "StartEnding";
    private string ending = "Ending";

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        eve = FindObjectOfType<EveController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAnim.SetBool(startEnding, true);
            playerAnim.SetBool(ending, true);
            eveAnim.SetBool(startEnding, true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetBool(startEnding, true);
            playerAnim.SetBool(ending, false);
            eveAnim.SetBool(startEnding, true);
        }
    }

    /// <summary>
    /// True
    /// </summary>
    public void TrueEndAnim()
    {
        player.anim.SetBool(startEnding, true);
        player.anim.SetBool(ending, true);
        eve.anim.SetBool(startEnding, true);
    }


    /// <summary>
    /// End
    /// </summary>
    public void BabEndAnim()
    {
        player.anim.SetBool(startEnding, true);
        player.anim.SetBool(ending, false);
        eve.anim.SetBool(startEnding, true);
    }
}
