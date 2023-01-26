using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    public bool tutFinishedFreeRoam = false;
    public bool tutFinishedEvac = false;
    public bool tutFinishedEx = false;

    public SavedData (GameManager gameManager)
    {
        tutFinishedFreeRoam = gameManager.tutFinishedFreeRoam;
        tutFinishedEvac = gameManager.tutFinishedEvac;
        tutFinishedEx = gameManager.tutFinishedEx;
    }
}
