using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridSquare currentlySelected { get; private set; } = null;

    public GridRow[] squares;

    public void SelectSquare(GridSquare square)
    {
        if (currentlySelected != null)
        {
            currentlySelected.Deselect();
        }

        currentlySelected = square;
        square.Select();

        Game.instance.OnSelectionChanged(this, square);
    }

    [System.Serializable]
    public class GridRow
    {
        public GridSquare[] squares;

        public GridRow(int i)
        {
            squares = new GridSquare[i];
        }

        public GridSquare this[int i] {
            get { return squares[i]; }
            set { squares[i] = value; }
        }
    }
}
