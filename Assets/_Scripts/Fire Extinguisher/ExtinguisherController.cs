using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using System;

public class ExtinguisherController : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool isSpraying = false;
    private Vector3 OGPosition;
    private Quaternion OGRotation;

    [SerializeField] private ExTypes exTypes;

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float increaseRate = 0;

    private void Start()
    {
        OGPosition = gameObject.GetComponent<Transform>().position;
        OGRotation = gameObject.GetComponent<Transform>().rotation;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(StartSpray);
        grabInteractable.deactivated.AddListener(StopSpray);
        grabInteractable.selectEntered.AddListener(DisableCollider);
        grabInteractable.selectExited.AddListener(ResetSpray);
    }


    private void OnDisable()
    {
        grabInteractable.activated.RemoveAllListeners();
        grabInteractable.deactivated.RemoveAllListeners();
        grabInteractable.selectEntered.RemoveAllListeners();
        grabInteractable.selectExited.RemoveAllListeners();
    }

    private void Extinguish()
    {
        if (Physics.Raycast(particleSystem.transform.position, particleSystem.transform.forward, out RaycastHit hit, 3f))
        {
            Debug.DrawRay(particleSystem.transform.position, particleSystem.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            
            hit.collider.TryGetComponent(out Fires fireOut);

            if (fireOut != null)
            {
                switch ((exTypes))
                {
                    case ExTypes.Water:
                        if (fireOut.FireType == Fires.FireTypes.Combustible)
                        {
                            //Debug.Log("Running Combustible Extinguish method");
                            fireOut.ReduceFireIntensity();
                        }
                        break;
                    case ExTypes.Foam:
                        if (fireOut.FireType != Fires.FireTypes.Electrical)
                        {
                            //Debug.Log("Running Foam Extinguish method");
                            fireOut.ReduceFireIntensity();
                        }
                        break;
                    case ExTypes.CO2:
                        if (fireOut.FireType != Fires.FireTypes.Combustible)
                        {
                            //Debug.Log("Running CO2 Extinguish method");
                            fireOut.ReduceFireIntensity();
                        }
                        break;
                }
            }                 
        }
    }

    private void StartSpray(ActivateEventArgs arg0)
    {

        if (!isSpraying)
        {
            StartCoroutine(IncreaseSprayRate());
        }
        
    }

    private void StopSpray(DeactivateEventArgs arg0)
    {
        if (isSpraying)
        {
            StartCoroutine(DecreaseSprayRate());
        }
    }

    private void DisableCollider(SelectEnterEventArgs arg0)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void ResetSpray(SelectExitEventArgs arg0)
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

        Debug.Log("stopping everything");

        StopAllCoroutines();
        var emissionRate = particleSystem.emission;
        emissionRate.rateOverTime = 0;
    }

    private IEnumerator DecreaseSprayRate()
    {
        isSpraying = false;

        var emissionRate = particleSystem.emission;
        emissionRate.rateOverTime = 0;

        yield return null;
    }

    public IEnumerator IncreaseSprayRate()
    {
        isSpraying = true;

        var emissionRate = particleSystem.emission;
        float currEmissionRate = 0;
        emissionRate.rateOverTime = 0;

        while (isSpraying)
        {
            currEmissionRate +=  increaseRate * Time.deltaTime;
            emissionRate.rateOverTime = currEmissionRate;

            Extinguish();

            if (currEmissionRate >= 1000)
            {
                currEmissionRate = 1000;
            }

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    public void ResetPosition()
    {
        if (grabInteractable.isSelected == false)
        {
            Debug.Log("Resetting position");

            gameObject.transform.position = OGPosition;
            gameObject.transform.rotation = OGRotation;
        }
        
    }

    public enum ExTypes
    {
        Water,
        Foam,
        CO2
    }
}
