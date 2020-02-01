using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoSingleton<Game>
{
    public Grid Grid1;
    public Grid Grid2;

    public Level[] Levels;

    private int _currentLevel = 0;
    public Level CurrentLevel
    {
        get
        {
            if (_currentLevel < 0 || _currentLevel >= Levels.Length)
            {
                return null;
            }
            return Levels[_currentLevel];
        }
    }

    private void Start()
    {
        CurrentLevel.DoStartLevel();
    }

    public void OnSelectionChanged(Grid grid, GridSquare newSelection)
    {
        if (CurrentLevel != null)
        {
            CurrentLevel.OnSelectionChanged(grid, newSelection);
        }
        CheckLevelFinished();
    }

    public void CheckLevelFinished()
    {
        if (CurrentLevel != null)
        {
            if (CurrentLevel.IsFinished())
            {
                CurrentLevel.DoWinLevel();
                GoToNextLevel();
            }
        }
    }

    public void GoToNextLevel()
    {
        _currentLevel++;
        if (CurrentLevel != null)
            CurrentLevel.DoStartLevel();
    }
}
