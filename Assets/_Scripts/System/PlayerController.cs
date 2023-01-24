using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float hitDelayTime;
    public CanvasGroup blindfoldCanvasGroup;

    
    private bool isHitDelay = false;
    private bool isDead = false;
    private int currentHealth;
    private Vector3 originalTransformPosition;
    private Quaternion originalTransformRotation;

    private void Start()
    {
        currentHealth = health;
        originalTransformPosition = gameObject.GetComponent<Transform>().position;
        originalTransformRotation = gameObject.GetComponent<Transform>().rotation;
    }

    public void TakeDamage()
    {
        Debug.Log("Taking damage");
        if (!isHitDelay)
        {
            currentHealth--;
            isHitDelay = true;
            StartCoroutine(HitDelay());
        }

        if (currentHealth == 0)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator HitDelay()
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

        yield return ToggleScreenBrightness();

        RespawnToOriginalPosition();

        yield return new WaitForSeconds(2);

        yield return ToggleScreenBrightness();

        locomotion.gameObject.SetActive(true);

        currentHealth = health;

        yield return null;
    }

    public void RespawnToOriginalPosition()
    {
        gameObject.GetComponent<Transform>().position = originalTransformPosition;
        gameObject.GetComponent<Transform>().rotation = originalTransformRotation;
    }

    IEnumerator ToggleScreenBrightness()
    {
        float currAlphaAmount = blindfoldCanvasGroup.alpha;

        if (!isDead)
        {
            while (currAlphaAmount <= 1)
            {
                currAlphaAmount += Time.deltaTime;
                blindfoldCanvasGroup.alpha = currAlphaAmount;
                yield return new WaitForEndOfFrame();
            }
            isDead = true;
            StopCoroutine(ToggleScreenBrightness());
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
}
