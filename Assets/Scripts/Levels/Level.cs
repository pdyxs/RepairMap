using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public Game game => Game.instance;
    public Grid Grid1 => game.Grid1;
    public Grid Grid2 => game.Grid2;

    public virtual void OnSelectionChanged(Grid grid, GridSquare newSelection)
    {

    }

    public abstract bool IsFinished();
}
