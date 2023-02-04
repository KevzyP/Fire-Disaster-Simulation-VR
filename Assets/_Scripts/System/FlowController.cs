using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowController : MonoBehaviour
{
    private GameManager gameManager;
    
    private PlayerController playerController;
    private ObjectiveController objectiveController;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        objectiveController = FindObjectOfType<ObjectiveController>();
        playerController = FindObjectOfType<PlayerController>();

        switch (gameManager.State)
        {
            case GameState.FreeRoamMode:
                StartCoroutine(FlowSequenceFreeRoam());
                break;
            case GameState.EvacMode:
                StartCoroutine(FlowSequenceEvac());
                break;
            case GameState.ExMode:
                StartCoroutine(FlowSequenceFireEx());
                break;
        }
    }

    private IEnumerator FlowSequenceFreeRoam()
    {
        Debug.Log("Begin FreeRoam Flow Sequence");

        TutorialPanelController tutorialPanelController = FindObjectOfType<TutorialPanelController>();

        playerController.ToggleLocomotionSystem();

        yield return new WaitUntil(() => gameManager.tutFinishedFreeRoam == true);
        tutorialPanelController.CloseTutorial();
        playerController.ToggleLocomotionSystem();

        yield return null;
    }

    private IEnumerator FlowSequenceFireEx()
    {
        TutorialPanelController tutorialPanelController = FindObjectOfType<TutorialPanelController>();
        FireController fireController = FindObjectOfType<FireController>();

        playerController.ToggleLocomotionSystem();

        yield return new WaitUntil(() => gameManager.tutFinishedEx == true);
        tutorialPanelController.CloseTutorial();
        playerController.ToggleLocomotionSystem();

        SetupFireEx();
        Objective fireObjective = objectiveController.objectiveList.Find((x) => x.CompareTag("FireObjective"));

        yield return new WaitUntil(() => fireObjective.objectiveFinished == true);

        Debug.Log("waited for objective to finish");

        yield return objectiveController.AddReturnToFreeRoamObjective();

        yield return null;
    }

    private IEnumerator FlowSequenceEvac()
    {
        TutorialPanelController tutorialPanelController = FindObjectOfType<TutorialPanelController>();
        FireController fireController = FindObjectOfType<FireController>();

        playerController.ToggleLocomotionSystem();

        yield return new WaitUntil(() => gameManager.tutFinishedEvac == true);
        tutorialPanelController.CloseTutorial();
        playerController.ToggleLocomotionSystem();

        SetupFireEvac();

        Objective evacObjective = objectiveController.objectiveList.Find((x) => x.CompareTag("EvacObjective") && !x.GetComponent<Objective>().objectiveFinished);

        yield return new WaitUntil(() => evacObjective.objectiveFinished);

        objectiveController.CheckForCompletion();
        objectiveController.AddEvacObjective();

        yield return null;
    }

    private void SetupFireEx()
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

    private void SetupFireEvac()
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