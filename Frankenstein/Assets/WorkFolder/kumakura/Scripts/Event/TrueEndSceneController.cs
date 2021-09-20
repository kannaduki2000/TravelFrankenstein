using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualShockInput;

public class TrueEndSceneController : MonoBehaviour
{
    private FadeControl fade;


    void Start()
    {
        fade = FindObjectOfType<FadeControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || DSInput.PushDown(DSButton.Circle))
        {
            fade.Fade("out", ()=> fade.sceneChange.SceneSwitching("MainTitle"));
        }
    }
}
