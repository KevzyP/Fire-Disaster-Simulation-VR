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
        TutorialPanelController tutorialPanelController = FindObjectOfType<TutorialPanelController>();
        FireController fireController = FindObjectOfType<FireController>();

        if (gameManager.tutFinishedEx)
        {
            tutorialPanelController.CloseTutorial();
        }

        yield return new WaitUntil(() => gameManager.tutFinishedEx == true);

        SetupFireEx();
        Objective fireObjective = objectiveController.objectiveList.Find((x) => x.CompareTag("FireObjective"));

        yield return new WaitUntil(() => fireObjective.objectiveFinished == true);

        Debug.Log("waited for objective to finish");

        yield return objectiveController.AddReturnToFreeRoamObjective();

        yield return null;
    }

    public IEnumerator FlowSequenceEvac()
    {
        TutorialPanelController tutorialPanelController = FindObjectOfType<TutorialPanelController>();
        FireController fireController = FindObjectOfType<FireController>();

        if (gameManager.tutFinishedEvac)
        {
            tutorialPanelController.CloseTutorial();
        }

        yield return new WaitUntil(() => gameManager.tutFinishedEvac == true);

        SetupFireEvac();

        Objective evacObjective = objectiveController.objectiveList.Find((x) => x.CompareTag("EvacObjective") && !x.GetComponent<Objective>().objectiveFinished);

        yield return new WaitUntil(() => evacObjective.objectiveFinished);

        objectiveController.CheckForCompletion();
        objectiveController.AddEvacObjective();

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

    public void SetupFireEvac()
    {
        Debug.Log("Running SetupFireEvac Method");
        FireController fireController = FindObjectOfType<FireController>();

        if (objectiveController == null)
        {
            objectiveController = FindObjectOfType<ObjectiveController>();
        }

        fireController.ActivateFires();
        objectiveController.AddEvacObjective();

    }
}