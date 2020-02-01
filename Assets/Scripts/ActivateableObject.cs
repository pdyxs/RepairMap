﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class ActivateableObject : MonoBehaviour
{
    public GridSquare parentSquare
    {
        get
        {
            if (_parentSquare == null)
            {
                _parentSquare = transform.GetComponentInParent<GridSquare>();
            }
            return _parentSquare;
        }
    }
    private GridSquare _parentSquare;

    public void Show()
    {
        CustomEvent.Trigger(gameObject, "Show");
    }

    public void Select()
    {
        CustomEvent.Trigger(gameObject, "Select");
    }

    public void Deselect()
    {
        CustomEvent.Trigger(gameObject, "Deselect");
    }

    public void Activate()
    {
        CustomEvent.Trigger(gameObject, "Activate");
    }
}
