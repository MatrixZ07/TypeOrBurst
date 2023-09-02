using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: GameManager als static class
public class GameManager : MonoBehaviour
{
    private State _currentState;

    public void SetState(State state)
    {
        _currentState= state;
        StartCoroutine(_currentState.Start());
    }

    public WordManager wordManager;
    public PlayerManager playerManager;
    public HighscoreHandler highscoreHandler;

    public UIManager uiManager;
    public HUDManager hudManager;
    private static bool gamestarted = false;
    public static bool GameStarted { get => gamestarted; }

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene"))
        {
            StartGame();
        }
	}

    public void StartGame()
    {
        CurrentScoreHandler.Reset();
        MultiplierHandler.Reset();
        playerManager.RevivePlayer();
        wordManager.SetInputPossible(true);
        hudManager.ShowGameUI(true);
        wordManager.StartWave();
        hudManager.ResumeGame();
        gamestarted = true;
    }

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
	public static void LoadMainMenuScene()
	{
		SceneManager.LoadScene(0);
	}
	public void IsPlayerDead(bool playerDead)
    {
        if (playerDead)
        {
            EndGame();
        }
    }

    //TODO: Lose-Condition in player implementieren. 
    public void EndGame() {
        Debug.Log("Trying to end game. in EndGame()");
        wordManager.SetInputPossible(false);
        wordManager.StopWave();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            GameObject.Destroy(enemy);
        }
        gamestarted = false;
        hudManager.PauseGame();
        hudManager.ShowGameOverScreen(true);
    }

	public void ExitApplication()
	{
		Application.Quit();
	}
}