public class Game : MonoSingleton<Game>
{
    public Grid Grid1;
    public Grid Grid2;

    public float endLevelDelay = 0.5f;

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

    public void StartGame()
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
                var lastLevel = CurrentLevel;
                var lastLevelIndex = _currentLevel;
                GoToNextLevel();
                TimeUtils.RunAfter(() =>
                {
                    lastLevel.DoWinLevel();
                    if (lastLevelIndex < Grid1.gate.Lights.Length)
                    {
                        Grid1.gate.Lights[lastLevelIndex].SetActive(true);
                    }
                    if (lastLevelIndex < Grid2.gate.Lights.Length)
                    {
                        Grid2.gate.Lights[lastLevelIndex].SetActive(true);
                    }
                    if (CurrentLevel != null)
                        CurrentLevel.DoStartLevel();
                }, endLevelDelay);
            }
        }
    }

    public void GoToNextLevel()
    {
        _currentLevel++;
    }
}
