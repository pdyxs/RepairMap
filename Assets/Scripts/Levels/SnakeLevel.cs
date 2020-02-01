using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Stand on end of snakes")]
public class SnakeLevel : Level
{
    public ActivateableObject Grid1ObjectPrefab; //1 straight //2 diag
    public ActivateableObject Grid2ObjectPrefab;//1 straight //2 diag

    public Coordinates[] Grid1ObjectLocations;
    public Coordinates[] Grid2ObjectLocations;

    private ActivateableObject[] Grid1Object
    {
        get
        {
            if (_grid1Object == null)
            {
                _grid1Object = returnObjectsToShow(Grid1ObjectLocations, Grid1ObjectPrefab); // GameObject.Instantiate(Grid1ObjectPrefab, Game.instance.Grid1.squares[y][x].transform);
            }
            return _grid1Object;
        }
    }
    private ActivateableObject[] _grid1Object;
    private ActivateableObject[] Grid2Object
    {
        get
        {
            if (_grid2Object == null)
            {
               _grid2Object = returnObjectsToShow(Grid2ObjectLocations, Grid2ObjectPrefab);
            }
            return _grid2Object;
        }
    }
    private ActivateableObject[] _grid2Object;


    ActivateableObject[] returnObjectsToShow(Coordinates[] locationObjets, ActivateableObject actObjects)
    {
        List<ActivateableObject> objectsReturn = new List<ActivateableObject>();
        foreach (Coordinates location in locationObjets)
        {
            objectsReturn.Add( GameObject.Instantiate(actObjects, Game.instance.Grid1.squares[location.y][location.x].transform));
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
        /*Grid1.currentlySelected != null &&
            Grid1.currentlySelected.MyObjects.Contains(Grid1Object) &&
            Grid2.currentlySelected != null &&
            Grid2.currentlySelected.MyObjects.Contains(Grid2Object);*/
    }
}
