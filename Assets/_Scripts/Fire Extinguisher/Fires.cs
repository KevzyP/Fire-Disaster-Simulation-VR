using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fires : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> fireParticles;
    [SerializeField] public FireTypes FireType;
    [SerializeField] private float fadeRate = 1;
    [SerializeField, Range(0f, 1f)] private float fireIntensity;

    private FireController fireController;
    private AudioSource audioSource;
    private float[] currentEmission;
    public bool isExtinguished = false;

    private void Update()
    {
    }

    // Use this for initialization
    void Start()
    {
        fireController = FindObjectOfType<FireController>();
        audioSource = gameObject.GetComponent<AudioSource>();

        currentEmission = new float[fireParticles.Count];

        for (int i = 0; i < fireParticles.Count; i++)
        {
            currentEmission[i] = fireParticles[i].emission.rateOverTime.constant;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }

    public void ReduceFireIntensity()
    {
        fireIntensity -= fadeRate * Time.deltaTime;
        if (fireIntensity <= 0)
        {
            fireIntensity = 0;
        }
        ChangeFireIntensity();
    }

    public void ChangeFireIntensity()
    {
        for (int i = 0; i < fireParticles.Count; i++)
        {
            ParticleSystem.EmissionModule emissionModule = fireParticles[i].emission;
            //currentEmission[i] -= fadeRate * Time.deltaTime;
            //emissionModule.rateOverTime = currentEmission[i];
            emissionModule.rateOverTime = fireIntensity * currentEmission[i];

            if (emissionModule.rateOverTime.constant <= 0)
            {
                isExtinguished = true;
            }
        }

        if (isExtinguished)
        {
            fireController.UpdateFireStatus();
            StartCoroutine(FadeOutSound());
        }
    }

    public void RandomizeSoundPitchAndPlaySound()
    {
        float randomPitchModifier = 0f;
        float audioPitch = 1f;

        randomPitchModifier = Random.Range(-0.5f, 0.5f);
        audioSource.pitch = (audioPitch + randomPitchModifier);
        audioSource.Play();
    }
    IEnumerator FadeOutSound()
    {
        float currentTime = 0f;
        float duration = 1.5f;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
        
        if (audioSource.volume == 0)
        {
            audioSource.Stop();
        }

        yield break;
    }

    public enum FireTypes
    {
        Combustible,
        FlammableLiquid,
        Electrical,
    }

}

