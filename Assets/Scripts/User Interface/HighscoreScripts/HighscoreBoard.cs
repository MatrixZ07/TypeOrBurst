using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HighscoreBoard : MonoBehaviour
{
    public HighscoreHandler highscoreHandler;

    public TextMeshProUGUI highscoresNames;
	public TextMeshProUGUI highscoresScores;

	private void Start()
	{
		UpdateDisplay();
	}
	public void UpdateDisplay()
    {
		if (highscoreHandler.highscores == null && !highscoreHandler.highscores.Any() )highscoreHandler.SetDefaultHighscores();

		string resultNames = "";
        string resultScores = "";

        for (int i = highscoreHandler.highscores.Count - 1; i >= 0; i--)
        {
			resultNames += highscoreHandler.highscores[i].playerName + "\n";
            resultScores += highscoreHandler.highscores[i].score.ToString() + "\n";   
        }

        highscoresNames.text = resultNames;
        highscoresScores.text = resultScores;
		Debug.Log("HighscoreBoard.UpdateDisplay() executed. Highscores displayed on HighscoreBoard.");
	}
}
