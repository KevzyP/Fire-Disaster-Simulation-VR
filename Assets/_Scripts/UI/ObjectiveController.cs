using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private Objective objectivePrefab;
    [SerializeField] private Transform objectiveParent;
    [SerializeField] public List<Objective> objectiveList;
    [TextArea (5, 10)]
    public List<string> fireEvacTexts;

    private string firesRemainingText;
    private int firesRemainingNumber;
    private int fireEvacTextsIndex;

    private void Start()
    {
        firesRemainingText = "Fires Extinguished: ";
        firesRemainingNumber = 0;

        fireEvacTextsIndex = 0;
    }

    public void AddFiresObjective()
    {
        FireController fireController = FindObjectOfType<FireController>();

        int fireListCount = fireController.fires.Count;
        string newText = firesRemainingText + firesRemainingNumber + " / " + fireListCount;

        Debug.Log("CompiledText is: " + newText);

        var newPrefab = Instantiate(objectivePrefab, objectiveParent);

        newPrefab.tag = "FireObjective";
        newPrefab.ChangeText(newText);
        objectiveList.Add(newPrefab);
    }

    public void AddEvacObjective()
    {
        var newPrefab = Instantiate(objectivePrefab, objectiveParent);

        newPrefab.tag = "EvacObjective";
        newPrefab.ChangeText(fireEvacTexts[fireEvacTextsIndex]);
        objectiveList.Add(newPrefab);

        fireEvacTextsIndex++;
    }


    public IEnumerator AddReturnToFreeRoamObjective()
    {
        var newPrefab = Instantiate(objectivePrefab, objectiveParent);
        string newText = "Return to the Free Roam room by using the front door of the building.";

        newPrefab.ChangeText(newText);
        objectiveList.Add(newPrefab);
        
        yield return null;
    }

    public void UpdateFireObjective()
    {
        FireController fireController = FindObjectOfType<FireController>();
        Objective fireObjective = objectiveList.Find((x) => x.CompareTag("FireObjective"));

        string compiledText = firesRemainingText + fireController.CheckExtinguishedFires() + " / " + fireController.fires.Count;
        fireObjective.ChangeText(compiledText);
    }

    public void CheckForCompletion()
    {
        Debug.Log("Running checkforcompletion");
        foreach (Objective objective in objectiveList)
        {
            if (objective.CheckIfObjectiveIsDone())
            {
                objective.ChangeTextColorToDoneColor();
            }
        }
    }
}
