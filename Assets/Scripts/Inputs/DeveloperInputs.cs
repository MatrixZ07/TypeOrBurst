using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperInputs : MonoBehaviour
{
	public float gameSpeed = 8f;
	public HighscoreHandler highscoreHandler;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			Time.timeScale = gameSpeed;
		}

		if (Input.GetKeyUp(KeyCode.F4))
		{
			highscoreHandler.DeleteHighscoreData();
			highscoreHandler.SetDefaultHighscores();
		}

		if (Input.GetKeyDown(KeyCode.F2))
		{
			Time.timeScale = 1f;
		}
	}
}
