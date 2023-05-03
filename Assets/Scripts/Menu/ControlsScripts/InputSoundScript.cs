using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputSoundScript : MonoBehaviour
{

    [SerializeField] private AudioSource textSound;

    void Start()
    {
        GetComponent<TMP_InputField>().onSelect.AddListener(OnSelect);
        GetComponent<TMP_InputField>().onDeselect.AddListener(OnDeselect);
    }

    void OnSelect(string text)
    {
        playSound();
    }

    void OnDeselect(string text)
    {
        playSound();
    }

    void playSound()
    {
        textSound.Play();
    }

}
