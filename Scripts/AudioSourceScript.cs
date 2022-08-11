using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;

public class AudioSourceScript : MonoBehaviour, IBackHandler, IHomeHandler
{
    private static AudioSourceScript instance;
    public static AudioSourceScript Instance { get { return instance; } }
    
    public AudioSource AudioBg;
    public GameObject AudioBgPanel;
    //public GameObject AudioSourceGameObj;
    
    // This bool will show ki kya audio turn on karni hai ya nahi; Initially false hogi i.e., nahi karni ON
    bool audioturnon = false;

    void Awake()
    {
        PlayTheAudio();
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        JMRInputManager.Instance.AddGlobalListener(gameObject);
    }

    private void PlayTheAudio()
    {
        AudioBg.Play();
    }

    public void AudioPlayerHitsEnemy()
    {
        Destroy(AudioBgPanel);
    }

    public void OnBackAction()
    {
        if (audioturnon == true)
        {
            PlayTheAudio();
            audioturnon = false;
        }

        else if (audioturnon == false)
        {
            AudioBg.Pause();
            audioturnon = true;
        }
    }

    public void OnHomeAction()
    {
        audioturnon = true;
        AudioBg.Pause();        
    }


}
