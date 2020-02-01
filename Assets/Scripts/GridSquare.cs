using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;

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

    public int x;
    public int y;

    public bool IsAt(int x, int y)
    {
        return this.x == x && this.y == y;
    }

    public bool isSelected { get; private set; } = false;

    public void Select()
    {
        isSelected = true;
        CustomEvent.Trigger(gameObject, "Select");
    }

    public void Deselect()
    {
        isSelected = false;
        CustomEvent.Trigger(gameObject, "Deselect");
    }
}
