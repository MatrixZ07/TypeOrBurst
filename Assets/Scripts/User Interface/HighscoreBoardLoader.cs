using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreBoardLoader : MonoBehaviour
{
    public HighscoreHandler highscoreHandler;

    public TextMeshProUGUI highscoresNames;
    public TextMeshProUGUI highscoresScores;

    public GameObject highscoreBoard;
	private void Start()
	{
		DisplayHighscores();
	}
	public void DisplayHighscores() //Reine Anzeige der Highscores im UI
    {
		Debug.Log("DisplayHighscores executed.");
		//Anzeige mit neuen Daten füttern
		string resultNames = "";
        string resultScores = "";
        for (int i = highscoreHandler.highscores.Count - 1; i >= 0; i--)
        {
            if (highscoreHandler.highscores[i] == null)
            {
				resultNames = "Error: Check Highscore Entries";
                return;
			}

			resultNames += highscoreHandler.highscores[i].playerName + "\n";
            resultScores += highscoreHandler.highscores[i].score.ToString() + "\n";
            
        }
        highscoresNames.text = resultNames;
        highscoresScores.text = resultScores;
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
