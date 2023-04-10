using UnityEngine;

public class BulletImpactParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] impactParticles;
    [SerializeField] private float timeToDestroy;

    private void OnEnable() 
    {
        PlayParticles();
        Invoke("DestroyParticles", timeToDestroy);
    }   


    private void PlayParticles()
    {
        foreach(ParticleSystem particle in impactParticles)
        {
            particle.Play();
            Debug.Log("Play particle system");
        }
    }


    private void DestroyParticles()
    {
        Destroy(this.gameObject);
    }

}
