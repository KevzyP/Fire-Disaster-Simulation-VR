using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowController : MonoBehaviour
{
    private ObjectiveController objectiveController;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        objectiveController = FindObjectOfType<ObjectiveController>();

        switch (gameManager.State)
        {
            case GameState.EvacMode:
                StartCoroutine(FlowSequenceEvac());
                break;
            case GameState.ExMode:
                StartCoroutine(FlowSequenceFireEx());
                break;
        }
    }

    public IEnumerator FlowSequenceFireEx()
    {
        FireController fireController = FindObjectOfType<FireController>();

        yield return new WaitUntil(() => gameManager.tutFinished == true);

        SetupFireEx();
        Objective fireObjective = objectiveController.objectiveList.Find((x) => x.CompareTag("FireObjective"));

        yield return new WaitUntil(() => fireObjective.objectiveFinished == true);

        Debug.Log("waited for objective to finish");

        yield return objectiveController.AddReturnToFreeRoamObjective();

        yield return null;
    }

    public IEnumerator FlowSequenceEvac()
    {
        yield return null;
    }

    public void SetupFireEx()
    {
        Debug.Log("Running SetupFireEx Method");
        FireController fireController = FindObjectOfType<FireController>();

        if (objectiveController == null)
        {
            objectiveController = FindObjectOfType<ObjectiveController>();
        }
        
        fireController.ActivateFires();
        objectiveController.AddFiresObjective();

    }
}