using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerManager playerManager;
    private GameObject[] gameUIElements;
    [SerializeField]
    public GameObject mainmenu;
    [SerializeField]
    public GameObject highscoresMenu;
    [SerializeField]
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public GameObject pauseMenuInstance;

    public GameObject gameoverScreen;
    
    public GameObject inputContainer;
    public GameObject highscoreBoard;
    public TextMeshProUGUI gameoverHeadline;

    public HighscoreManager highscoreManager;

    //F�r GameOverScreen
    public TextMeshProUGUI highscoresNames;
    public TextMeshProUGUI highscoresScores;
    public TextMeshProUGUI yourScore;
    public TMP_InputField userInput;

    public GameObject highscoreContainerPrefab;
    public GameObject highscoreContainer;

    public GameObject highscoresMenuBoard;

    public TextMeshProUGUI waveDisplay;
    public bool showWaveDisplay = false;

    private void Awake()
    {
        gameUIElements = GameObject.FindGameObjectsWithTag("GameUI");
        //waveDisplay.gameObject.SetActive(false);
    }
    private void Start()
    {
        
    }
    private void Update()
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

    public void ShowMainMenu(bool show)
    {
        if (playerManager!=null && playerManager.playerSprite != null) { 
            playerManager.RevivePlayer();
        }
        if (show)
        {
            mainmenu.SetActive(true);
            ShowHighscores(false);
            ShowSettings(false);
            ShowGameOverScreen(false);
            ShowGameUI(false);
            ShowPauseMenu(false);
        }
        else {
            mainmenu.SetActive(false); 
        }
        
    }

    public void ShowGameUI(bool show)
    {
        if (show)
        {
            foreach (GameObject uielement in gameUIElements)
            {
                uielement.SetActive(true);
            }
            ShowGameOverScreen(false);
            ShowMainMenu(false);
        }
        else
        {
            foreach (GameObject uielement in gameUIElements)
            {
                uielement.SetActive(false);

            }

        }
    }

    public void ShowGameOverScreen(bool show) { //POST GAME ANZEIGE (GameOver/NewHighscore)

        if (show)
        {
            gameoverScreen.SetActive(true);
            if(gameoverScreen!=null)
            highscoreContainer = Instantiate(highscoreContainerPrefab,highscoreContainerPrefab.transform.localPosition, Quaternion.identity,gameoverScreen.transform) as GameObject;
            if (highscoreManager.isNewHighscore())
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

    public void ShowSettings(bool show) {
        if (show)
        {
            settingsMenu.SetActive(true);
            ShowMainMenu(false);
        }
        else {
            if(settingsMenu != null)
            settingsMenu.SetActive(false);
        }
    }
    public void ShowHighscores(bool show) //HIGHSCOREANZEIGE VOM HAUPTMEN�
    {
        if (show)
        {
            highscoresMenu.SetActive(true);
            ShowMainMenu(false);
            if (GameObject.Find("HighscoresMenu") != null) {
                highscoresMenuBoard = Instantiate(highscoreContainerPrefab, highscoreContainerPrefab.transform.localPosition, Quaternion.identity, highscoresMenu.transform) as GameObject;
                highscoresMenuBoard.transform.localPosition = new Vector3(highscoresMenuBoard.transform.localPosition.x, highscoresMenuBoard.transform.localPosition.y - 50f, highscoresMenuBoard.transform.localPosition.z);
                highscoresMenuBoard.GetComponent<HighscoreBoardLoader>().ShowInputContainer(false); //�ber ShowInputContainer(false) wird auch DisplayHighscores() aufgerufen und die Anzeige mit den Highscores gef�ttert.
            }
        }
        else
        { 
            if(highscoresMenuBoard != null)
            Destroy(highscoresMenuBoard);
            if(highscoresMenu != null)
            highscoresMenu.SetActive(false);
        }

    }

    public void ShowPauseMenu(bool show) {
        if (show)
        {
            pauseMenuInstance = Instantiate(pauseMenu,GameObject.Find("Canvas").transform);
        }
        else {
            Destroy(pauseMenuInstance);
        }
    }

    public void ShowWaveDisplay(int wave) {
        waveDisplay.text = "Wave " + wave.ToString();
        waveDisplay.gameObject.SetActive(true);
        showWaveDisplay = true;
    }
    
}
