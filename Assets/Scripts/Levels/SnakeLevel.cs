using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Stand on end of snakes")]
public class SnakeLevel : Level
{
    //1 left
    //2 up
    //3 diag left
    //4 diag right
    //public ActivateableObject[] Grid1ObjectPrefab;
   // public ActivateableObject[] Grid2ObjectPrefab;

    public SnakeSet[] Grid1Snake;
    public SnakeSet[] Grid2Snake;

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
                _grid1Object = returnObjectsToShow(Grid1Snake, Game.instance.Grid1); // GameObject.Instantiate(Grid1ObjectPrefab, Game.instance.Grid1.squares[y][x].transform);
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





    public override bool IsFinished()
    {
        return false;
    }
}

[System.Serializable]
public class SnakeSet
{
    public ActivateableObject GridObject;
    public Coordinates Location;

}