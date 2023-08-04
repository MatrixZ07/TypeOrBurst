using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreInputBoardLoader : MonoBehaviour
{
    
    public TextMeshProUGUI yourScore;
    public TMP_InputField userInput;

    public GameObject inputContainer;
	public HighscoreBoardLoader highscoresContainer;

    //Rename. Macht zwei verschiedene Sachen. Name nicht aussagekräftig.
	public void ShowInputContainer(bool show) {
        if (show)
        {
            inputContainer.SetActive(true);
            highscoresContainer.gameObject.SetActive(false);
            yourScore.text = CurrentScoreHandler.CurrentScore.ToString();
        }
        else {
            inputContainer.SetActive(false);
            highscoresContainer.gameObject.SetActive(true);
            highscoresContainer.DisplayHighscores();
        }
    }

    public void SubmitHighscore() //Übertrag des Namens und speicherung des Namens und Highscores in Datei
    {
        //Ist highscore wirklich ein neuer Highscore? DANN wird gespeichert
        //necessary when Submit button is only displayed when Highscore is reached?
        if (highscoresContainer.highscoreHandler.isNewHighscore() )
        {
            //Wurde ein Name eingegeben?
            if (userInput.text != "")
            {
                //Highscore updaten und speichern
                highscoresContainer.highscoreHandler.SaveHighscores(userInput.text);
                inputContainer.SetActive(false);
                highscoresContainer.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("No username Input registered - Highscore not saved, try again");
            } 
        } 
    }
}
