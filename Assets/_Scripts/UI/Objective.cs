using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class Objective : MonoBehaviour
{
    [SerializeField] private Color doneColor;

    private TextMeshProUGUI objectiveText;

    public bool objectiveFinished = false;

    public void ChangeText(string text)
    {
        if (objectiveText == null)
            objectiveText = gameObject.GetComponent<TextMeshProUGUI>();

        objectiveText.text = text;
    }

    public void ChangeTextColorToDoneColor()
    {
        objectiveText.color = doneColor;
    }


    public bool CheckIfObjectiveIsDone()
    {
        if (objectiveFinished)
        {
            Debug.Log("Objective finished");
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
}
