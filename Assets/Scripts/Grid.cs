using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridSquare currentlySelected { get; private set; } = null;

    public void SelectSquare(GridSquare square)
    {
        if (currentlySelected != null)
        {
            currentlySelected.Deselect();
        }

        currentlySelected = square;
        square.Select();
    }
}
