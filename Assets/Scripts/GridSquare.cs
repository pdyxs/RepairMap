using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    public Grid grid {
        get
        {
            if (_grid == null)
            {
                _grid = transform.parent.GetComponent<Grid>();
            }
            return _grid;
        }
    }
    private Grid _grid;

    public bool isSelected { get; private set; } = false;

    public void Select()
    {
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
    }
}
