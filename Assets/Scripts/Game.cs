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
                TimeUtils.RunAfter(() =>
                {
                    CurrentLevel.DoWinLevel();
                    if (_currentLevel < Grid1.gate.Lights.Length)
                    {
                        Grid1.gate.Lights[_currentLevel].SetActive(true);
                    }
                    if (_currentLevel < Grid2.gate.Lights.Length)
                    {
                        Grid2.gate.Lights[_currentLevel].SetActive(true);
                    }
                    GoToNextLevel();
                }, endLevelDelay);
                
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
