using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSamePlaceLevel : Level
{
    public int targetX;
    public int targetY;

    public override bool IsFinished()
    {
        return Grid1.currentlySelected != null &&
            Grid1.currentlySelected.IsAt(targetX, targetY) &&
            Grid2.currentlySelected != null &&
            Grid2.currentlySelected.IsAt(targetX, targetY);
    }
}
