using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRControllersHandler : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor;

    private XRDirectInteractor directInteractor;

    private bool rayActive = false;

    private void SwitchInteractorType()
    {
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out RaycastHit hit, 15f) && hit.transform.gameObject.CompareTag("UI"))
        {
            Debug.Log("Raycast hit");
            if (!rayActive)
            {
                directInteractor.enabled = false;
                rayInteractor.enabled = true;
            }
            
            rayActive = true;
        }

        else
        {
            if (rayActive)
            {
                rayInteractor.enabled = false;
                directInteractor.enabled = true;
            }
            
            rayActive = false;
        }
    }

    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        directInteractor = gameObject.GetComponent<XRDirectInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("drawing ray");
        SwitchInteractorType();
    }
}
