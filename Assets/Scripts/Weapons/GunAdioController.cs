using UnityEngine;

public class GunAdioController : MonoBehaviour
{

    [SerializeField] private AudioClip clip;
    private AudioSource audioSource;
    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayGunAudio()
    {
        audioSource.PlayOneShot(clip);
    }

}
