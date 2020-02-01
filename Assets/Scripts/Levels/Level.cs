using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public abstract class Level : ScriptableObject
{
    public Game game => Game.instance;
    public Grid Grid1 => game.Grid1;
    public Grid Grid2 => game.Grid2;

    public abstract ActivateableObject[] ObjectsToShow { get; }

    public abstract ActivateableObject[] ObjectsToActivate { get; }

    public virtual void OnSelectionChanged(Grid grid, GridSquare newSelection)
    {

    }

    public abstract bool IsFinished();

    public void DoStartLevel()
    {
        foreach (var obj in ObjectsToShow)
        {
            obj.Show();
        }
        OnStarted();
    }

    protected virtual void OnStarted() { }

    public void DoWinLevel()
    {
        foreach (var obj in ObjectsToActivate)
        {
            obj.Activate();
        }
    }
}
