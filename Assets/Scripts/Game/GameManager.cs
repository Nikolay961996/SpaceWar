using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _currentLevelNeedScore = 10;
    [SerializeField] private int _currentLevel = 1;

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _loadLevelPanel;
    [SerializeField] private GameObject _winPanel;
    
    private int _currentScore;
    private bool _gamePaused;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public int GetTotalNeedScore() => _currentLevelNeedScore;
    public int GetCurrentScore() => _currentScore;

    public int GetCurrentLevel() => _currentLevel;

    public bool AddScore(int addScore)
    {
        _currentScore += addScore;
        var isNextLevel = _currentScore >= _currentLevelNeedScore;
        if (isNextLevel)
        {
            NextLevel();
            return true;
        }

        return false;
    }

    public void Pause()
    {
        _gamePaused = !_gamePaused;
        _pausePanel.SetActive(_gamePaused);
        if (_gamePaused)
            GamePause();
        else
            GameResume();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameResume();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
        GameResume();
    }

    public void GameOver()
    {
        GamePause();
        _gameOverPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void NextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)
            Win();
        else
            StartCoroutine(nameof(LoadNewLevel));
    }

    private IEnumerator LoadNewLevel()
    {
        GamePause();
        _loadLevelPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);
        
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

        _loadLevelPanel.SetActive(false);

        GameResume();
    }

    private void Win()
    {
        GamePause();
        _winPanel.SetActive(true);
    }

    private void GamePause()
    {
        Time.timeScale = 0;
    }

    private void GameResume()
    {
        Time.timeScale = 1;
    }
}
