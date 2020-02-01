using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSameObjectLevel : Level
{
    public ActivateableObject Grid1Object;
    public ActivateableObject Grid2Object;

    public override ActivateableObject[] ObjectsToShow => new [] { Grid1Object, Grid2Object };

    public override ActivateableObject[] ObjectsToActivate => new[] { Grid1Object, Grid2Object };

    public override bool IsFinished()
    {
        return Grid1.currentlySelected != null &&
            Grid1.currentlySelected.MyObjects.Contains(Grid1Object) &&
            Grid2.currentlySelected != null &&
            Grid2.currentlySelected.MyObjects.Contains(Grid2Object);
    }
}
