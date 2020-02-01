using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Stand on end of snakes")]
public class SnakeLevel : Level
{
    public ActivateableObject[] Grid1ObjectPrefabs;
    public ActivateableObject[] Grid2ObjectPrefabs;


    private ActivateableObject Grid1Object
    {
        get
        {
            if (_grid1Object == null)
            {
                foreach (ActivateableObject Grid1ObjectPrefab in Grid1ObjectPrefabs)
                {
                    _grid1Object = GameObject.Instantiate(Grid1ObjectPrefab, Game.instance.Grid1.squares[y][x].transform);
                }
            }
            return _grid1Object;
        }
    }
    private ActivateableObject _grid1Object;
    private ActivateableObject Grid2Object
    {
        get
        {
            if (_grid2Object == null)
            {
                foreach (ActivateableObject Grid2ObjectPrefab in Grid2ObjectPrefabs)
                {
                    _grid2Object = GameObject.Instantiate(Grid2ObjectPrefab, Game.instance.Grid2.squares[y][x].transform);
                }
            }
            return _grid2Object;
        }
    }
    private ActivateableObject _grid2Object;

    public int x;
    public int y;

    public override ActivateableObject[] ObjectsToShow => new[] { Grid1Object, Grid2Object };

    public override ActivateableObject[] ObjectsToActivate => new[] { Grid1Object, Grid2Object };





    public override bool IsFinished()
    {
        return Grid1.currentlySelected != null &&
            Grid1.currentlySelected.MyObjects.Contains(Grid1Object) &&
            Grid2.currentlySelected != null &&
            Grid2.currentlySelected.MyObjects.Contains(Grid2Object);
    }
}
