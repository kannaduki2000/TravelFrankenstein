using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour
{
    AudioSource audioSource;
    public FadeControl fade;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        VolumeChange();
    }

    // Update is called once per frame
    void Update()
    {
        if(fade.hiyoko == true)
        {
            
        }
    }

    public void VolumeChange()
    {
    }

    IEnumerator VolumeDown()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
