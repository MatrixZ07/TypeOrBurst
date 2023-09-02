using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreInputBoard : MonoBehaviour
{
    
    public TextMeshProUGUI yourScore;
    public TMP_InputField userInput;

    public GameObject inputContainer;
	public HighscoreBoard highscoresBoard;
	public HighscoreHandler highscoreHandler;

	//Rename. Macht zwei verschiedene Sachen. Name nicht aussagekräftig.
	public void ShowInputContainer(bool show) {
        if (show)
        {
            inputContainer.SetActive(true);
            highscoresBoard.gameObject.SetActive(false);
            yourScore.text = CurrentScoreHandler.CurrentScore.ToString();
        }
        else {
            inputContainer.SetActive(false);
            highscoresBoard.gameObject.SetActive(true);
            highscoresBoard.UpdateDisplay();
			yourScore.text = CurrentScoreHandler.CurrentScore.ToString();
		}
    }

    public void SubmitHighscore() 
    {
        if (highscoreHandler.isNewHighscore() ) //TODO: Rework access to HighscoreHandler
        {
            if (userInput.text != "")
            {
                highscoreHandler.SaveHighscores(userInput.text);
                inputContainer.SetActive(false);
                highscoresBoard.gameObject.SetActive(true);
				highscoresBoard.UpdateDisplay();
			}
            else
            {
                Debug.Log("No username Input registered - Highscore not saved, try again");
            } 
        } 
    }
}
