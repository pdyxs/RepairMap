using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Colours")]
public class ColoursLevel : Level
{
    public override ActivateableObject[] ObjectsToShow => throw new System.NotImplementedException();

    public override ActivateableObject[] ObjectsToActivate => throw new System.NotImplementedException();

    public ActivateableObject CompletePrefab;

    public List<ColourSetup> Setups;

    public override bool IsFinished()
    {
        throw new System.NotImplementedException();
    }
}

public class ColourSetup
{
    public ActivateableObject Grid1Object;
    public ActivateableObject Grid2Object;

    public List<Coordinates> Coordinates;
}

[System.Serializable]
public class Coordinates
{
    int x;
    int y;

    public GridSquare Square(Grid grid)
    {
        return grid.squares[y][x];
    }
}
