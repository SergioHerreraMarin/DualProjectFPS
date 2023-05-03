using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonScript : MonoBehaviour
{

    [SerializeField] private AudioSource buttonSound;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        buttonSound.Play();
    }

}
