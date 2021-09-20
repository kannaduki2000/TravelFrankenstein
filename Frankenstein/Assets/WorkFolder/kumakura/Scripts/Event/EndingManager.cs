using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DebugLogUtility;

public class EndingManager : MonoBehaviour
{

    private static string objectName = "EndingManager";
    private static EndingManager instance = null;
    public static EndingManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject mamagerObject = new GameObject(objectName);
                instance = mamagerObject.AddComponent<EndingManager>();
            }
            return instance;
        }
    }


    private EndingAnimationContol endAnimCon;

    private void Awake()
    {
        endAnimCon = FindObjectOfType<EndingAnimationContol>();
    }

    void Update()
    {
        
    }


    public void Ending()
    {
        float playerHp = FindObjectOfType<PlayerController>()?.HP ?? 0;

        // HPが20以上の時
        if (20 <= playerHp)
        {
            // trueEnd
            endAnimCon.TrueEndAnim();
        }
        // それ以下の時
        else
        {
            // badEnd
            endAnimCon.BabEndAnim();
        }
    }
}
