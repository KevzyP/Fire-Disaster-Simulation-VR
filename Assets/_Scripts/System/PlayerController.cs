using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private float hitDelayTime;

    [SerializeField] private InputActionReference leftJoystick;
    [SerializeField] private InputActionReference leftTrigger;
    [SerializeField] private InputActionReference rightTrigger;

    [SerializeField] private AudioClip UIClickSound;
    [SerializeField] private AudioClip footstepsSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private CanvasGroup blindfoldCanvasGroup;

    [SerializeField] private XRInteractorLineVisual leftRayInteractorVisual;
    [SerializeField] private XRInteractorLineVisual rightRayInteractorVisual;

    private AudioSource audioSource;
    private bool isHitDelay = false;
    private bool isDead = false;
    private int currentHealth;
    private Vector3 originalTransformPosition;
    private Quaternion originalTransformRotation;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = health;
        originalTransformPosition = gameObject.GetComponent<Transform>().position;
        originalTransformRotation = gameObject.GetComponent<Transform>().rotation;
    }
    private void OnEnable()
    {
        leftJoystick.action.started += EnableFootsteps;
        leftJoystick.action.canceled += DisableFootsteps;
        leftTrigger.action.started += ClickSound;
        rightTrigger.action.started += ClickSound;
    }


    private void OnDisable()
    {
        leftJoystick.action.started -= EnableFootsteps;
        leftJoystick.action.canceled -= DisableFootsteps;
        leftTrigger.action.started -= ClickSound;
        rightTrigger.action.started -= ClickSound;
    }


    public void TakeDamage()
    {
        if (!isHitDelay)
        {
            Debug.Log("Taking damage");
            currentHealth--;
            if (currentHealth == 0)
            {
                StartCoroutine(PlayerDeath());
                DeathSound();
            }
            else
            {
                StartCoroutine(ReddenScreen());
                HurtSound();

                isHitDelay = true;
                StartCoroutine(HitDelay());
            }
            
        }
    }

    public void RespawnToOriginalPosition()
    {
        gameObject.GetComponent<Transform>().position = originalTransformPosition;
        gameObject.GetComponent<Transform>().rotation = originalTransformRotation;
    }

    private IEnumerator HitDelay()
    {
        Debug.Log("Delaying...");
        yield return new WaitForSeconds(hitDelayTime);
        Debug.Log("Delay done.");
        isHitDelay = false;
    }
   
    public IEnumerator PlayerDeath()
    {
        LocomotionSystem locomotion = FindObjectOfType<LocomotionSystem>();
        locomotion.gameObject.SetActive(false);

        yield return DeathScreen();

        RespawnToOriginalPosition();

        yield return new WaitForSeconds(2);

        yield return DeathScreen();

        locomotion.gameObject.SetActive(true);

        currentHealth = health;

        yield return null;
    }

    private IEnumerator DeathScreen()
    {
        float currAlphaAmount = blindfoldCanvasGroup.alpha;
        Image blindfoldImage = blindfoldCanvasGroup.GetComponentInChildren<Image>();
        blindfoldImage.color = Color.black;

        if (!isDead)
        {
            while (currAlphaAmount <= 1)
            {
                
                currAlphaAmount += Time.deltaTime;
                blindfoldCanvasGroup.alpha = currAlphaAmount;
                yield return new WaitForEndOfFrame();
            }
            isDead = true;
            StopCoroutine(DeathScreen());
        }

        else if (isDead)
        {
            while (currAlphaAmount >= 0)
            {
                currAlphaAmount -= Time.deltaTime;
                blindfoldCanvasGroup.alpha = currAlphaAmount;
                yield return new WaitForEndOfFrame();
            }
            isDead = false;
        }

        
        yield return null;
    }

    private IEnumerator ReddenScreen()
    {
        float currAlphaAmount = 0;
        Image blindfoldImage = blindfoldCanvasGroup.GetComponentInChildren<Image>();

        blindfoldImage.color = Color.red;

        while (currAlphaAmount <= 0.3)
        {
            currAlphaAmount += Time.deltaTime;
            blindfoldCanvasGroup.alpha = currAlphaAmount;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (currAlphaAmount >= 0)
        {
            currAlphaAmount -= Time.deltaTime;
            blindfoldCanvasGroup.alpha = currAlphaAmount;
            yield return null;
        }

        yield return null;
    }

    private void HurtSound()
    {
        audioSource.clip = hurtSound;
        audioSource.Play();
    }

    private void DeathSound()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
    }

    private void EnableFootsteps(InputAction.CallbackContext obj)
    {
        audioSource.clip = footstepsSound;
        audioSource.Play();
    }

    private void DisableFootsteps(InputAction.CallbackContext obj)
    {
        audioSource.Stop();
    }

    private void ClickSound(InputAction.CallbackContext obj)
    {
        if (leftRayInteractorVisual.reticle.activeInHierarchy || rightRayInteractorVisual.reticle.activeInHierarchy)
        {
            audioSource.clip = UIClickSound;
            audioSource.Play();
        }        
    }   
}
