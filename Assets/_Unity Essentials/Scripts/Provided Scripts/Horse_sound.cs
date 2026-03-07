using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Horse_sound : MonoBehaviour
{
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;
    public string triggerTag = "Ball";

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(triggerTag))
        {
            float impactSpeed = collision.relativeVelocity.magnitude;

            if (impactSpeed > 0.2f)
            {
                audioSource.volume = Mathf.Clamp01(impactSpeed / 5f);
                audioSource.pitch = Random.Range(minPitch, maxPitch);
                audioSource.Play();
            }
        }
    }
}