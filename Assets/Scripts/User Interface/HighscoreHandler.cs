using UnityEngine;
using System.IO;
using Newtonsoft.Json;
//Diese Klasse kümmert sich um Highscores.
public class HighscoreHandler : MonoBehaviour
{
    public HighscoreEntry[] highscores;
    private string HIGHSCORE_DATA_PATH;
    public bool HighscoreSaved { get; private set; }
    private void Start()
    {
        HIGHSCORE_DATA_PATH = Application.persistentDataPath + "/highscores.json";
        LoadHighscores();
    }

    
    public void UpdateHighscores(int highscore, string playerName)
    { //Update der HighscoreEinträge mit dem neuen Highscore ( Called in SaveHighscores())
        HighscoreEntry newvalue = new HighscoreEntry(highscore, playerName);
        HighscoreEntry _temp;
        for (int i = highscores.Length - 1; i >= 0; i--)
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
    public void LoadHighscores()
    { //Laden der Highscores aus der JSON datei
        try
        {
            if (!File.Exists(HIGHSCORE_DATA_PATH))
            {
                SetDefaultHighscores();
            }
            HighscoreData data = JsonConvert.DeserializeObject<HighscoreData>(File.ReadAllText(HIGHSCORE_DATA_PATH));
            for (int i = 0; i < highscores.Length; i++)
            {
                highscores[i] = data.entries[i];
            }
        }
        //catch (FileNotFoundException e)
        //{
        //    FileNotFoundExceptions are handled here.
        //    if (e.Source != null)
        //        Debug.Log(e.Message);
        //}
        catch (IOException e)
        {
            // Extract some information from this exception, and then
            // throw it to the parent method.
            if (e.Source != null)
                Debug.Log(e.Message);
            throw;
        }

    }
    public void DeleteHighscoreData()
    {
        foreach (HighscoreEntry entry in highscores)
        {
            entry.playerName = "n.a";
            entry.score = 0;
        }
        HighscoreData data = new HighscoreData();
        data.entries = highscores;
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(HIGHSCORE_DATA_PATH, jsonData);
        //LoadHighscores();
    }
    public void SetDefaultHighscores()
    {
        HighscoreEntry[] defaultHighscoreEntries =
        {
            new HighscoreEntry(0,"Nobody"),
            new HighscoreEntry(0,"Nobody"),
            new HighscoreEntry(0,"Nobody"),
            new HighscoreEntry(0,"Nobody"),
            new HighscoreEntry(0,"Nobody"),
        };
        highscores = defaultHighscoreEntries;
        HighscoreData defaultData = new HighscoreData();
        defaultData.entries = defaultHighscoreEntries;
        string jsonData = JsonConvert.SerializeObject(defaultData);
        File.WriteAllText(HIGHSCORE_DATA_PATH, jsonData);
        LoadHighscores();
    }
    
}

//Hilfsklasse, die HighscoreEntry-s in JSON speichert.
[System.Serializable]
public class HighscoreData 
{
    public HighscoreEntry[] entries = new HighscoreEntry[5];
}

//Hilfsklasse zur Speicherung von Highscores
[System.Serializable]
public class HighscoreEntry
{ 
    public int score;
    public string playerName;

    public HighscoreEntry(int s, string p)
    {
        score = s;
        playerName = p;
    }
}
