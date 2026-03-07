using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BallSound : MonoBehaviour
{
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed > 0.5f)
        {
            audioSource.volume = Mathf.Clamp01(impactSpeed / 10f);
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
    }
}