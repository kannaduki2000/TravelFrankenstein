using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualShockInput;

public class TrueEndSceneController : MonoBehaviour
{
    private FadeControl fade;
    private float time = 0;
    private bool trigger = true;

    void Start()
    {
        fade = FindObjectOfType<FadeControl>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.Return) || DSInput.PushDown(DSButton.Circle)) && time > 5 && trigger)
        {
            trigger = false;
            fade.Fade("out", ()=> fade.sceneChange.SceneSwitching("MainTitle"));
        }
    }
}
