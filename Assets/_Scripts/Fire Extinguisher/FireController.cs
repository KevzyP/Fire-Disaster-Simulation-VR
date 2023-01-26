using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public List<Fires> fires;
    public List<AudioSource> fireSounds;
    private ObjectiveController objectiveController;

    public void UpdateFireStatus()
    {
        objectiveController.UpdateFireObjective();
    }

    public int CheckExtinguishedFires()
    {
        int currentFiresExtinguished = 0;
        for (int i = 0; i < fires.Count; i++)
        {
            if (fires[i].isExtinguished)
            {
                currentFiresExtinguished++;
            }
        }

        if (currentFiresExtinguished == fires.Count)
        {
            Objective fireObjective = objectiveController.objectiveList.Find((x) => x.CompareTag("FireObjective"));
            fireObjective.objectiveFinished = true;
            objectiveController.CheckForCompletion();
        }

        return currentFiresExtinguished;
    }

    private void Start()
    {
        objectiveController = FindObjectOfType<ObjectiveController>();
        fires.AddRange(gameObject.GetComponentsInChildren<Fires>());

        StartCoroutine(DeactivateFires());
    }

    public void ActivateFires()
    {
        for (int i = 0; i < fires.Count; i++)
        {
            Fires thisFire = fires[i].gameObject.GetComponent<Fires>();
            thisFire.gameObject.SetActive(true);
            
            if (thisFire.gameObject.GetComponent<AudioSource>() != null)
            {
                AudioSource audioSource = thisFire.gameObject.GetComponent<AudioSource>();
                Debug.Log("Playing audiosource");
                thisFire.RandomizeSoundPitchAndPlaySound();
            }


        }
    }

    public IEnumerator DeactivateFires()
    {
        foreach (Fires fireObject in fires)
        {
            Debug.Log("Deactivating fires...");
            fireObject.gameObject.SetActive(false);
            yield return null;
        }
    }

    
}
