using System.Collections;
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
                if (_parentSquare != null)
                    _parentSquare.MyObjects.Add(this);
            }
            return _parentSquare;
        }
    }
    private GridSquare _parentSquare;

    public void Hide()
    {
        CustomEvent.Trigger(gameObject, "Hide");
    }

    public void Show()
    {
        var p = parentSquare;
        CustomEvent.Trigger(gameObject, "Show");
    }

    public void Select()
    {
        CustomEvent.Trigger(gameObject, "Select");
    }

    public void MarkComplete()
    {
        CustomEvent.Trigger(gameObject, "MarkComplete");
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
