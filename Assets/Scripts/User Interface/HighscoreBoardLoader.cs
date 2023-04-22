using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreBoardLoader : MonoBehaviour
{
    public HighscoreHandler highscoreHandler;

    public TextMeshProUGUI highscoresNames;
    public TextMeshProUGUI highscoresScores;
    public TextMeshProUGUI yourScore;
    public TMP_InputField userInput;

    public GameObject inputContainer;
    public GameObject highscoreBoard;
    private void Awake()
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
        //Übertrag der Highscores in das UI
        yourScore.text = CurrentScoreHandler.CurrentScore.ToString();
        //Anzeige mit neuen Daten füttern
        string resultNames = "";
        string resultScores = "";
        int placeIndex = 1;
        for (int i = highscoreHandler.highscores.Length - 1; i >= 0; i--)
        {
            if (highscoreHandler.highscores[i] != null)
            {
                if (placeIndex == 1)
                {
                    string s = placeIndex.ToString() + "  : " + highscoreHandler.highscores[i].playerName + "\n";
                    resultNames += (highscoreHandler.highscores[i] != null) ? s  : "n.a. \n";

                }
                else {
                    string s = placeIndex.ToString() + " : " + highscoreHandler.highscores[i].playerName + "\n";
                    resultNames += (highscoreHandler.highscores[i] != null) ? s : "n.a. \n";
                }
                resultScores += highscoreHandler.highscores[i].score.ToString() + " \n";
                placeIndex++;
            }
        }
        highscoresNames.text = resultNames;
        highscoresScores.text = resultScores;
    }

    public void SubmitHighscore() //Übertrag des Namens und speicherung des Namens und Highscores in Datei
    {
        //Ist highscore wirklich ein neuer Highscore? DANN wird gespeichert
        if (highscoreHandler.isNewHighscore() )
        {
            //Wurde ein Name eingegeben?
            if (userInput.text != "" || userInput.text == null)
            {
                //Highscore updaten und speichern
                highscoreHandler.SaveHighscores(userInput.text);
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
