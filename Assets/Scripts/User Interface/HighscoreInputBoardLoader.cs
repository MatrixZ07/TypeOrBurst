using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreInputBoardLoader : HighscoreBoardLoader
{
    
    public TextMeshProUGUI yourScore;
    public TMP_InputField userInput;

    public GameObject inputContainer;

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
}
