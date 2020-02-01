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

    public (ActivateableObject, ActivateableObject) CreateAt(ActivateableObject prefab1, ActivateableObject prefab2, int x, int y)
    {
        return (CreateAt(prefab1, 1, x, y), CreateAt(prefab2, 2, x, y));
    }

    public (ActivateableObject, ActivateableObject) CreateAt(ActivateableObject prefab1, ActivateableObject prefab2, Coordinates coords)
    {
        return CreateAt(prefab1, prefab2, coords.x, coords.y);
    }

    public ActivateableObject CreateAt(ActivateableObject prefab, int grid, Coordinates coords)
    {
        return CreateAt(prefab, grid, coords.x, coords.y);
    }

    public ActivateableObject CreateAt(ActivateableObject prefab, int grid, int x, int y)
    {
        return CreateAt(prefab, Game.instance[grid].squares[y][x]);
    }

    public ActivateableObject CreateAt(ActivateableObject prefab, GridSquare square)
    {
        return GameObject.Instantiate(prefab, square.transform);
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

[System.Serializable]
public class Coordinates
{
    public int x;
    public int y;

    public GridSquare Square(Grid grid)
    {
        return grid.squares[y][x];
    }
}
