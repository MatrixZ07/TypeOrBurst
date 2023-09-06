using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*used to display on screen menus
 */
public class HUDManager : MonoBehaviour
{
    private GameObject[] gameUIElements;
    
    public TextMeshProUGUI waveDisplay;
    [SerializeField]
    private bool showWaveDisplay = false;
    
    //PauseMenu
    public GameObject pauseMenu;
    public GameObject pauseMenuInstance;

    //GameOverScreen
    public GameObject gameoverScreen;
    public GameObject highscoreInputBoard;
    public HighscoreHandler highscoreHandler;
    public TextMeshProUGUI gameoverHeadline;

    private void Awake()
    {
        gameUIElements = GameObject.FindGameObjectsWithTag("GameUI");
        waveDisplay.gameObject.SetActive(false);
    }
    private void Start()
    {
        ShowGameUI(true);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (gamepaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
		MoveWaveDisplay();
    }
    
    //InGame-HUD
    public void ShowGameUI(bool show)
    {
        if (show)
        {
            foreach (GameObject uielement in gameUIElements)
            {
                uielement.SetActive(true);
            }
            ShowGameOverScreen(false);
        }
        else
        {
            foreach (GameObject uielement in gameUIElements)
            {
                uielement.SetActive(false);
            }

        }
    }
    
    public void ShowWaveDisplay(int waveIndex) {
        waveDisplay.text = "Wave " + waveIndex.ToString();
        waveDisplay.gameObject.SetActive(true);
        showWaveDisplay = true;
    }
    

    private void MoveWaveDisplay()
    {
        if (!waveDisplay.gameObject.activeInHierarchy)
        {
            return;
        }
        if (waveDisplay.transform.localPosition.y > 0)
        {
            waveDisplay.transform.localPosition -= new Vector3(0f, 30f * Time.deltaTime, 0f);
        }
        else
        {
            waveDisplay.gameObject.SetActive(false);
            showWaveDisplay = false;
            waveDisplay.transform.localPosition = new Vector3(0f, 30f, 0f);
        }
    }
    
    
    //InGame
    public void ShowPauseMenu(bool show) {
        if (show)
        {
            pauseMenuInstance = Instantiate(pauseMenu,GameObject.Find("Canvas").transform);
        }
        else {
            Destroy(pauseMenuInstance);
        }
    }

	public void ResumeGame()
	{
		ShowPauseMenu(false);
		Time.timeScale = 1;
		gamepaused = false;
	}

	public void PauseGame()
	{
		if (!GameManager.GameStarted) return;

		ShowPauseMenu(true);

		Time.timeScale = 0;
		gamepaused = true;
		Debug.Log("GAME PAUSED.");
		//TODO: GameState wechseln.
	}



	public void ShowGameOverScreen(bool show) //POST GAME ANZEIGE (GameOver/NewHighscore)
    { 
        gameoverScreen.SetActive(show);
        highscoreInputBoard.SetActive(show);

        gameoverHeadline.text = (highscoreHandler.isNewHighscore()) ? "New Highscore" : "Game Over";
        highscoreInputBoard.GetComponent<HighscoreInputBoard>().ShowInputContainer(highscoreHandler.isNewHighscore());
    }

	private bool gamepaused = true;
}
