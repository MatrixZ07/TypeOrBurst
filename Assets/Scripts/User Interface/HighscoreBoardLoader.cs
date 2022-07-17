using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreBoardLoader : MonoBehaviour
{
    public HighscoreManager highscoreManager;

    public TextMeshProUGUI highscoresNames;
    public TextMeshProUGUI highscoresScores;
    public TextMeshProUGUI yourScore;
    public TMP_InputField userInput;

    public GameObject inputContainer;
    public GameObject highscoreBoard;
    private void Awake()
    {
        //Get HighscoreManager und UIManager
        highscoreManager = GameObject.FindGameObjectWithTag("HighscoreManager").GetComponent<HighscoreManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowInputContainer(bool show) {
        if (show)
        {
            inputContainer.SetActive(true);
            highscoreBoard.SetActive(false);
        }
        else {
            inputContainer.SetActive(false);
            highscoreBoard.SetActive(true);
            DisplayHighscores();
        }
    }

    public void DisplayHighscores() //Reine Anzeige der Highscores im UI
    { 
        //�bertrag der Highscores in das UI
        yourScore.text = highscoreManager.GetHighscore().ToString();
        //Anzeige mit neuen Daten f�ttern
        string resultNames = "";
        string resultScores = "";
        int placeIndex = 1;
        for (int i = highscoreManager.highscores.Length - 1; i >= 0; i--)
        {
            if (highscoreManager.highscores[i] != null)
            {
                if (placeIndex == 1)
                {
                    string s = placeIndex.ToString() + "  : " + highscoreManager.highscores[i].playerName + "\n";
                    resultNames += (highscoreManager.highscores[i] != null) ? s  : "n.a. \n";

                }
                else {
                    string s = placeIndex.ToString() + " : " + highscoreManager.highscores[i].playerName + "\n";
                    resultNames += (highscoreManager.highscores[i] != null) ? s : "n.a. \n";
                }
                resultScores += highscoreManager.highscores[i].score.ToString() + " \n";
                placeIndex++;
            }
        }
        highscoresNames.text = resultNames;
        highscoresScores.text = resultScores;
    }

    public void SubmitHighscore() //�bertrag des Namens und speicherung des Namens und Highscores in Datei
    {
        //Ist highscore wirklich ein neuer Highscore? DANN wird gespeichert
        if (highscoreManager.isNewHighscore() )
        {
            //Wurde ein Name eingegeben?
            if (userInput.text != "" || userInput.text == null)
            {
                //Highscore updaten und speichern
                highscoreManager.SaveHighscores(userInput.text);
                inputContainer.SetActive(false);
                highscoreBoard.SetActive(true);
            }
            else
            {
                Debug.Log("No username Input registered - Highscore not saved, try again");
            } 
        } 
    }

    public void PlayClick()
    {
        FindObjectOfType<AudioManager>().Play("UIClick");
    }

    public void PlayHoverUI()
    {
        FindObjectOfType<AudioManager>().Play("UIHover");
    }
}
