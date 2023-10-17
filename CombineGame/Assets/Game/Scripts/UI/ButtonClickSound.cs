using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    private AudioController audioController; 
    void Start()
    {
        audioController = AudioController.instance;
        Button button = GetComponent<Button>();
        button.onClick.AddListener( () => audioController.audioSource.PlayOneShot(audioController.uiClickSound));
    }
}
