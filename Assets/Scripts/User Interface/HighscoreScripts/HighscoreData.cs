using System.Collections.Generic;

//Hilfsklasse, die HighscoreEntry-s in JSON speichert.
[System.Serializable]
public class HighscoreData
{
	public List<HighscoreEntry> entries = new List<HighscoreEntry>();

	public HighscoreData()
	{

	}
	public HighscoreData(HighscoreEntry entry1,
						 HighscoreEntry entry2,
						 HighscoreEntry entry3,
						 HighscoreEntry entry4,
						 HighscoreEntry entry5)
	{
		entries.Add(entry1);
		entries.Add(entry2);
		entries.Add(entry3);
		entries.Add(entry4);
		entries.Add(entry5);
	}

}
