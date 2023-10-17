using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource audioSource;

    public AudioClip uiClickSound;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Destroying duplicate AudioController object - only one is allowed per scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        SetAudioState(PlayerPrefs.GetInt(Keys.soundEnabled) == 0);
        GameAction.changeSoundState += SetAudioState;
    }

    private void OnDestroy()
    {
        GameAction.changeSoundState -= SetAudioState;
    }

    void SetAudioState(bool audioStatus)
    {
        audioSource.enabled = false; //audioStatus; //todo ввішькнути після того як додам звуки
    }
}
