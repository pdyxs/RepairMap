using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Colours")]
public class ColoursLevel : Level
{
    public override ActivateableObject[] ObjectsToShow
    {
        get
        {
            var ret = new List<ActivateableObject>();
            foreach (var setup in Setups)
            {
                if (!setup.HasCreatedObjects)
                    setup.CreateObjects(this);
                ret.AddRange(setup.Grid1Objects);
                ret.AddRange(setup.Grid2Objects);
            }
            return ret.ToArray();
        }
    }

    public override ActivateableObject[] ObjectsToActivate {
        get {
            var ret = new List<ActivateableObject>(Grid1CreatedIndicators.Values);
            ret.AddRange(Grid2CreatedIndicators.Values);
            return ret.ToArray();
        }
    }

    public int numberToWin = 3;

    public ActivateableObject IndicatorObjectPrefab;

    public ActivateableObject CompletePrefab;

    public List<ColourSetup> Setups;

    protected override void OnStarted()
    {
        base.OnStarted();
        Grid1CreatedIndicators.Clear();
        Grid2CreatedIndicators.Clear();
    }

    public override void OnSelectionChanged(Grid grid, GridSquare newSelection)
    {
        base.OnSelectionChanged(grid, newSelection);
        var grid1Prefab = PrefabAtCoords(Grid1.currentlySelected.x, Grid1.currentlySelected.y, 1);
        if (grid1Prefab == null)
        {
            return;
        }
        var grid2Prefab = PrefabAtCoords(Grid2.currentlySelected.x, Grid2.currentlySelected.y, 2);
        if (grid2Prefab != grid1Prefab)
        {
            return;
        }

        if (!Grid1CreatedIndicators.ContainsKey(grid1Prefab))
        {
            var newIndicator = GameObject.Instantiate(IndicatorObjectPrefab, Grid1.currentlySelected.transform);
            newIndicator.Show();
            Grid1CreatedIndicators[grid1Prefab] = newIndicator;
        }

        if (!Grid2CreatedIndicators.ContainsKey(grid2Prefab))
        {
            var newIndicator = GameObject.Instantiate(IndicatorObjectPrefab, Grid2.currentlySelected.transform);
            newIndicator.Show();
            Grid2CreatedIndicators[grid2Prefab] = newIndicator;
        }
    }

    private Dictionary<ActivateableObject, ActivateableObject> Grid1CreatedIndicators = new Dictionary<ActivateableObject, ActivateableObject>();
    private Dictionary<ActivateableObject, ActivateableObject> Grid2CreatedIndicators = new Dictionary<ActivateableObject, ActivateableObject>();

    public override bool IsFinished()
    {
        return Grid1CreatedIndicators.Count == numberToWin;
    }

    public ActivateableObject PrefabAtCoords(int x, int y, int grid)
    {
        foreach (var setup in Setups)
        {
            foreach (var coord in setup.Coordinates)
            {
                if (coord.x == x && coord.y == y)
                {
                    if (grid == 1)
                    {
                        return setup.Grid1Object;
                    }
                    return setup.Grid2Object;
                }
            }
        }
        return null;
    }
}

[System.Serializable]
public class ColourSetup
{
    public ActivateableObject Grid1Object;
    public ActivateableObject Grid2Object;

    public bool HasCreatedObjects => Grid1Objects != null && !Grid1Objects.Contains(null) && Grid1Objects.Count == Coordinates.Count &&
        Grid2Objects != null && !Grid2Objects.Contains(null) && Grid2Objects.Count == Coordinates.Count;

    public List<ActivateableObject> Grid1Objects { get; private set; } = null;
    public List<ActivateableObject> Grid2Objects { get; private set; } = null;

    public void CreateObjects(Level level)
    {
        Grid1Objects = new List<ActivateableObject>();
        Grid2Objects = new List<ActivateableObject>();
        foreach (var coord in Coordinates)
        {
            var o1 = GameObject.Instantiate(Grid1Object, Game.instance.Grid1.squares[coord.y][coord.x].transform);
            var o2 = GameObject.Instantiate(Grid2Object, Game.instance.Grid2.squares[coord.y][coord.x].transform);
            Grid1Objects.Add(o1);
            Grid2Objects.Add(o2);
        }
    }

    public List<Coordinates> Coordinates;
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
