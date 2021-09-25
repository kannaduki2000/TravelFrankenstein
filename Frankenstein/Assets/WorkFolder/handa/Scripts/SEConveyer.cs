using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEConveyer : MonoBehaviour
{
    public static SEConveyer instance = null;

    private AudioSource audioSource = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySE(AudioClip clip)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("nai");
        }
    }

    public void StopSE(AudioClip clip)
    {
        if(audioSource != null)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.Log("nai");
        }
    }
}
