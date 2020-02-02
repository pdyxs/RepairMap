using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class Gate : MonoBehaviour
{
    public GameObject[] Lights;

    public void TurnOn()
    {
        CustomEvent.Trigger(gameObject, "TurnOn");
    }
}
