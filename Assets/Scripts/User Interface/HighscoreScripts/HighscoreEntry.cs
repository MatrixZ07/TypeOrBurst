//Hilfsklasse zur Speicherung von Highscores
[System.Serializable]
public class HighscoreEntry
{
	public int score;
	public string playerName;

	public HighscoreEntry(int s = 0, string p = "DefaultBot")
	{
		score = s;
		playerName = p;
	}
}