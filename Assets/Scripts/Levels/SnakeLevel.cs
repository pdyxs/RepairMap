using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Stand on end of snakes")]
public class SnakeLevel : Level
{
    public SnakeSet[] Grid1Snake;
    public SnakeSet[] Grid2Snake;

    public Coordinates startCoords;
    public Coordinates endCoords;

    public ActivateableObject completedObjectPrefab;

    private void OnEnable()
    {
        _grid1Object = null;
        _grid2Object = null;
    }

    private ActivateableObject[] Grid1Object
    {
        get
        {
            if (_grid1Object == null)
            {
                _grid1Object = returnObjectsToShow(Grid1Snake, Game.instance.Grid1);
            }
            return _grid1Object;
        }
    }
    private ActivateableObject[] _grid1Object = null;

    private ActivateableObject[] Grid2Object
    {
        get
        {
            if (_grid2Object == null)
            {
               _grid2Object = returnObjectsToShow(Grid2Snake, Game.instance.Grid2);
            }
            return _grid2Object;
        }
    }
    private ActivateableObject[] _grid2Object = null;


    ActivateableObject[] returnObjectsToShow(SnakeSet[] locationObjets, Grid grid)
    {
        List<ActivateableObject> objectsReturn = new List<ActivateableObject>();
        foreach (SnakeSet snakeSet in locationObjets) 
        {
            objectsReturn.Add( GameObject.Instantiate(snakeSet.GridObject, grid.squares[snakeSet.Location.y][snakeSet.Location.x].transform));
        }

        return objectsReturn.ToArray();
    }

    private ActivateableObject[] GridObjects
    {
        get
        {
            List<ActivateableObject> returnList = new List<ActivateableObject>(Grid1Object);

            returnList.AddRange(Grid2Object);

            return returnList.ToArray();
        }
    }

    public override ActivateableObject[] ObjectsToShow => GridObjects;

    public override ActivateableObject[] ObjectsToActivate => new ActivateableObject[] {};

    public override void OnSelectionChanged(Grid grid, GridSquare newSelection)
    {
        base.OnSelectionChanged(grid, newSelection);
        if (Grid1.currentlySelected == null || Grid2.currentlySelected == null) return;
        if (Game.instance[1].IsAt(startCoords) || Game.instance[1].IsAt(endCoords) ||
            Game.instance[2].IsAt(startCoords) || Game.instance[2].IsAt(endCoords))
        {
            foreach (var obj in Grid1Object)
            {
                obj.MarkComplete();
            }
            foreach (var obj in Grid2Object)
            {
                obj.MarkComplete();
            }
        } else
        {
            foreach (var obj in Grid1Object)
            {
                obj.UnmarkComplete();
            }
            foreach (var obj in Grid2Object)
            {
                obj.UnmarkComplete();
            }
        }
    }


    public override bool IsFinished()
    {
        return (Game.instance[1].IsAt(startCoords) && Game.instance[2].IsAt(endCoords)) ||
            (Game.instance[2].IsAt(startCoords) && Game.instance[1].IsAt(endCoords));
    }
}

[System.Serializable]
public class SnakeSet
{
    public ActivateableObject GridObject;
    public Coordinates Location;
}