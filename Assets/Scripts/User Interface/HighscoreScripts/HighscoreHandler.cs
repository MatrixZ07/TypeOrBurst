using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
public class HighscoreHandler : MonoBehaviour
{

    public List<HighscoreEntry> highscores;
    private string HIGHSCORE_DATA_PATH;
    public bool HighscoreSaved { get; private set; }

	private void Awake()
	{
		HIGHSCORE_DATA_PATH = Application.persistentDataPath + "/highscores.json";
        LoadHighscores();
	}

	public void UpdateHighscores(int highscore, string playerName)
    { //Update der HighscoreEinträge mit dem neuen Highscore ( Called in SaveHighscores())
        HighscoreEntry newvalue = new HighscoreEntry(highscore, playerName);
        HighscoreEntry _temp;
        for (int i = highscores.Count - 1; i >= 0; i--)
        {
            if (highscores[i] == null)
            {
                highscores[i] = newvalue;
                break;
            }
            else if (newvalue.score >= highscores[i].score)
            {
                _temp = highscores[i];
                highscores[i] = newvalue;
                newvalue = _temp;
            }
        }
    }
    public void SaveHighscores(string playerName)
    { //Speichern der Highscores in JSON Datei
        HighscoreData data = new HighscoreData();
        UpdateHighscores(CurrentScoreHandler.CurrentScore, playerName);
        data.entries = highscores;
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(HIGHSCORE_DATA_PATH, jsonData);
        HighscoreSaved = true;
    }

    public bool isNewHighscore()
    { //Checkt, ob aktueller Score ein neuer Highscore ist
        foreach (HighscoreEntry entry in highscores)
        {
            if (entry == null) return true;
            if (CurrentScoreHandler.CurrentScore >= entry.score) return true;
        }
        return false;
    }
	//Laden der Highscores aus der JSON datei
	public void LoadHighscores()
    {
        if (!File.Exists(HIGHSCORE_DATA_PATH))
        {
            SetDefaultHighscores();
        }
        HighscoreData data = JsonConvert.DeserializeObject<HighscoreData>(File.ReadAllText(HIGHSCORE_DATA_PATH));
        highscores = data.entries;
    }
    public void DeleteHighscoreData()
    {
        HighscoreData data = new HighscoreData();
        highscores = data.entries;
		string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(HIGHSCORE_DATA_PATH, jsonData);
        //LoadHighscores();
    }
    public void SetDefaultHighscores()
    {
        HighscoreData defaultData = new HighscoreData(new HighscoreEntry(), new HighscoreEntry(), new HighscoreEntry(), new HighscoreEntry(), new HighscoreEntry());
        highscores = defaultData.entries;
        string jsonData = JsonConvert.SerializeObject(defaultData);
        File.WriteAllText(HIGHSCORE_DATA_PATH, jsonData);
    }

}
