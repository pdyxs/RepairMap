using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Go to Same Object")]
public class GoToSameObjectLevel : Level
{
    public ActivateableObject Grid1ObjectPrefab;
    public ActivateableObject Grid2ObjectPrefab;

    public List<Stage> stages;

    public bool allAtOnce = false;

    private int StagesComplete { get; set; } = 0;

    private void OnEnable()
    {
        StagesComplete = 0;
    }

    protected override void OnStarted()
    {
        base.OnStarted();
        StagesComplete = 0;
        foreach (var stage in stages)
        {
            stage.isComplete = false;
        }
    }

    public override ActivateableObject[] ObjectsToShow {
        get
        {
            if (!allAtOnce)
            {
                if (stages.Count == 0) return new ActivateableObject[] { };

                return stages[0].Objects(this);
            } 
            else
            {
                return ObjectsToActivate;
            }
        }
    }

    public override ActivateableObject[] ObjectsToActivate {
        get
        {
            var ret = new List<ActivateableObject>();
            foreach (var stage in stages)
            {
                ret.AddRange(stage.Objects(this));
            }
            return ret.ToArray();
        }
    }

    public override void OnSelectionChanged(Grid grid, GridSquare newSelection)
    {
        base.OnSelectionChanged(grid, newSelection);
        if (allAtOnce)
        {
            foreach (var stage in stages)
            {
                if (!stage.isComplete && stage.isSatisfied())
                {
                    MarkComplete(stage);
                    break;
                }
            }
        } 
        else
        {
            if (StagesComplete < stages.Count && stages[StagesComplete].isSatisfied())
            {
                MarkComplete(stages[StagesComplete]);
            }
        }
    }

    private void MarkComplete(Stage stage)
    {
        stage.MarkComplete();
        StagesComplete++;
        
        if (!allAtOnce && StagesComplete < stages.Count)
        {
            foreach (var obj in stages[StagesComplete].Objects(this))
            {
                obj.Show();
            }
        }
    }

    public override bool IsFinished()
    {
        return StagesComplete == stages.Count;
    }

    [System.Serializable]
    public class Stage
    {
        public Coordinates coords;

        public bool isComplete { get; set; } = false;
        
        public ActivateableObject Grid1Object { get; private set; }
        public ActivateableObject Grid2Object { get; private set; }

        public ActivateableObject[] Objects(GoToSameObjectLevel level)
        {
            if (Grid1Object == null || Grid2Object == null)
                Create(level);
            return new[] { Grid1Object, Grid2Object };
        }

        public void MarkComplete()
        {
            isComplete = true;
            Grid1Object.MarkComplete();
            Grid2Object.MarkComplete();
        }

        public bool isSatisfied()
        {
            return Game.instance.AreBothAt(coords);
        }

        public void Create(GoToSameObjectLevel level)
        {
            (Grid1Object, Grid2Object) = level.CreateAt(level.Grid1ObjectPrefab, level.Grid2ObjectPrefab, coords);
        }
    }
}
