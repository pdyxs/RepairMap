using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{

    public int[,] DefaultGrid;

    public Transform characterMovementTrans;
    public Transform characterRotationTrans;

    private void Start()
    {
        if(characterMovementTrans == null)
        {
            Debug.LogError("set characterMovementTrans in Placement");
        }

        if (characterRotationTrans == null)
        {
            Debug.LogError("set characterRotationTrans in Placement");
        }
    }



}
