using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public WordManager wordManager;
    public  PlayerManager playerManager;
    public HighscoreManager highscoreManager;

    [SerializeField] private float gamespeed = 1;
    private bool gamepaused = true;

    public UIManager uiManager;
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
        uiManager = gameObject.GetComponent<UIManager>();
        uiManager.ShowGameUI(false);
        uiManager.ShowMainMenu(true);
        uiManager.ShowGameOverScreen(false);
    }

    // Update is called once per frame
    void Update()
    {
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
            highscoreManager.DeleteHighscoreData();
        }
    }

    public void StartGame() {
        highscoreManager.ResetHighscore();
        playerManager.RevivePlayer();
        wordManager.SetInputPossible(true);
        uiManager.ShowGameUI(true);
        wordManager.StartWave();
        ResumeGame();
        gamestarted = true;
    }

    public void isPlayerDead(bool playerDead)
    {
        if (playerDead)
        {
            EndGame();
        }
    }
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
        uiManager.ShowGameOverScreen(true);
    }

    public void PauseGame() {
        if(gamestarted)
            uiManager.ShowPauseMenu(true);
        Debug.Log("GAME PAUSED.");
        Time.timeScale = 0;
        gamepaused = true;
    }
    public void ResumeGame() {
        uiManager.ShowPauseMenu(false);
        Time.timeScale = 1;
        gamepaused = false;
    }

    public void ExitApplication() {
        Application.Quit();
    }
}
