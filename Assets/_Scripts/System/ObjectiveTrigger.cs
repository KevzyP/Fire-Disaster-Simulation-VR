using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("TOUCHDOWN");
            ObjectiveController objectiveController = FindObjectOfType<ObjectiveController>();

            objectiveController.objectiveList.Find((x) => x.CompareTag("EvacObjective") && !x.GetComponent<Objective>().objectiveFinished).objectiveFinished = true;
            gameObject.SetActive(false);   
        }
    }


}
