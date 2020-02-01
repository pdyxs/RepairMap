using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridBuilder : MonoBehaviour
{
    public int width;
    public int height;

    public Vector2 size;

    public GridSquare GridSquarePrefab;

    public Grid grid
    {
        get
        {
            if (_grid == null)
            {
                _grid = GetComponent<Grid>();
            }
            return _grid;
        }
    }
    private Grid _grid;
}
