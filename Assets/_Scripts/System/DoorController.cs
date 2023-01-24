using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform insidePosition;
    [SerializeField] private Transform outsidePosition;

    private bool isInside;


    // Start is called before the first frame update
    void Start()
    {
        isInside = true;
    }

    public void TeleportPlayerToOtherSide()
    {
        Vector3 playerPos = new();
        Quaternion playerRot = new();

        if (isInside)
        {
            playerPos = outsidePosition.position;
            playerRot = outsidePosition.rotation;

            playerTransform.position = playerPos;
            playerTransform.rotation = playerRot;

            isInside = false;            
        }

        else
        {
            playerPos = insidePosition.position;
            playerRot = insidePosition.rotation;

            playerTransform.position = playerPos;
            playerTransform.rotation = playerRot;

            isInside = true;
        }
    }


}
