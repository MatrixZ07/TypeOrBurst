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

    #region old implementation
    public WordManager wordManager;
    public PlayerManager playerManager;
    public HighscoreHandler highscoreHandler;

    [SerializeField] private float gamespeed = 1;
    private bool gamepaused = true;

    public UIManager uiManager;
    public HUDManager hudManager;
    private bool gamestarted;

    private void Awake()
    {
        PauseGame();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        FindObjectOfType<AudioManager>().Play("MenuSoundtrack");
        if(SceneManager.GetActiveScene()==SceneManager.GetSceneByName("GameScene")) StartGame();
        if (uiManager == null) return;
        //uiManager = gameObject.GetComponent<UIManager>();
        //uiManager.ShowMainMenu(true);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Auslagern in DevControls Klasse
        if (Input.GetKeyDown(KeyCode.F1))
        {
            gamespeed += 0.5f;
            Time.timeScale = gamespeed;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            gamespeed -= 0.5f;
            Time.timeScale = gamespeed;
        }
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (gamepaused)
            {
                ResumeGame();
            }
            else { 
                PauseGame();
            }
        }
        if (Input.GetKeyUp(KeyCode.F4)) {
            highscoreHandler.DeleteHighscoreData();
        }
    }

    public void StartGame()
    {
        //Das alles beim Laden der neuen Szene ausführen
        CurrentScoreHandler.Reset();
        playerManager.RevivePlayer();
        wordManager.SetInputPossible(true);
        hudManager.ShowGameUI(true);
        wordManager.StartWave();
        ResumeGame();
        gamestarted = true;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void isPlayerDead(bool playerDead)
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
        PauseGame();
        hudManager.ShowGameOverScreen(true);
    }

    //TODO: PauseGame in hudManager auslagern. 
    public void PauseGame() {
        if(gamestarted)
            hudManager.ShowPauseMenu(true);
        Debug.Log("GAME PAUSED.");
        Time.timeScale = 0;
        gamepaused = true; //TODO: GameState wechseln.
    }

    public void ResumeGame() {
        hudManager.ShowPauseMenu(false);
        Time.timeScale = 1;
        gamepaused = false;
    }

    public void ExitApplication() {
        Application.Quit();
    }

#endregion
}

