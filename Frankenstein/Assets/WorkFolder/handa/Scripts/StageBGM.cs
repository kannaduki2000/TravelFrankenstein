using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBGM : MonoBehaviour
{
    public static StageBGM BGM = null;

    private void Awake()
    {
        if(BGM == null)
        {
            BGM = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(SceneManager.GetActiveScene().name == "MainTitle")
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
