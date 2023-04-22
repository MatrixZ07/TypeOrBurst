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
    public bool showWaveDisplay = false;
    
    //PauseMenu
    public GameObject pauseMenu;
    public GameObject pauseMenuInstance;

    //GameOverScreen
    public GameObject gameoverScreen;
    public GameObject highscoreContainer;
    public GameObject highscoreContainerPrefab;
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
    
    public void ShowWaveDisplay(int wave) {
        waveDisplay.text = "Wave " + wave.ToString();
        waveDisplay.gameObject.SetActive(true);
        showWaveDisplay = true;
    }
    
    private void MoveWaveDisplay()
    {
        if (waveDisplay != null && showWaveDisplay == true)
        {
            if (waveDisplay.gameObject.activeInHierarchy)
            {
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
    
    

    
    
    public void ShowGameOverScreen(bool show) { //POST GAME ANZEIGE (GameOver/NewHighscore)

        if (show)
        {
            gameoverScreen.SetActive(true);
            if(gameoverScreen!=null)
                highscoreContainer = Instantiate(highscoreContainerPrefab,
                    highscoreContainerPrefab.transform.localPosition, Quaternion.identity,
                    gameoverScreen.transform) as GameObject;
            if (highscoreHandler.isNewHighscore())
            {
                gameoverHeadline.text = "New Highscore";
                //Zeige InputContainer. �ber InputContainer's Submit wird dann highscoreBoard angezeigt.
                highscoreContainer.GetComponent<HighscoreBoardLoader>().ShowInputContainer(true);
            }
            else
            {
                gameoverHeadline.text = "Game Over";
                highscoreContainer.GetComponent<HighscoreBoardLoader>().ShowInputContainer(false);
            }
        }
        else
        {
            gameoverScreen.SetActive(false);
            Destroy(highscoreContainer);
        }
    }
}
