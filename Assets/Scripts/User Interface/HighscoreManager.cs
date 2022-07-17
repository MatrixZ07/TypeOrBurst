using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class HighscoreManager : MonoBehaviour
{
    public TextMeshProUGUI highscoreDisplay;
    public TextMeshProUGUI multiplierDisplay;


    int highscore = 0;
    public int  multiplier = 1;
    [SerializeField] private int multiplierCap = 10;

    public HighscoreEntry[] highscores = new HighscoreEntry[5];
    public bool highscoreSaved = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetHighscore();
        LoadHighscores();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetHighscore() {
        return highscore;
    }
    public void ResetHighscore() {
        highscore = 0;
        multiplier = 1;
        SetMultiplier();
        highscoreDisplay.text = highscore.ToString();
    }
    public void Add(int value) { //Fügt Wert auf aktuellen Highscore hinzu
        highscore = highscore + value * multiplier;
        highscoreDisplay.text = highscore.ToString();
    }

    public void IncreaseMultiplier() //Erhöht aktuellen Multiplier
    {
        if(multiplier<multiplierCap)  multiplier = multiplier + 3;
        SetMultiplier();
    }
    public void DecreaseMultiplier() { //Senkt aktuellen Multiplier
        if (multiplier > 1) {
            multiplier = (multiplier-3 < 0) ? 0: multiplier - 3;
        }
        SetMultiplier();
    }
    public void SetMultiplier() { //Setzt Multiplier in GameUI
        multiplierDisplay.text = multiplier.ToString() + "x";
    }

    public bool isNewHighscore() { //Checkt, ob aktueller Score ein neuer Highscore ist
        foreach (HighscoreEntry entry in highscores) {
            if (entry == null) return true;
            if (highscore >= entry.score) return true;
        }
        return false;
    }

    public void LoadHighscores() { //Laden der Highscores aus der JSON datei
        try
        {
            HighscoreData data = JsonUtility.FromJson<HighscoreData>(File.ReadAllText(Application.persistentDataPath + "/highscores.json"));
            for (int i = 0; i < highscores.Length; i++)
            {
                highscores[i] = data.entries[i];
            }
        }
        catch (FileNotFoundException e)
        {
            // FileNotFoundExceptions are handled here.
            if (e.Source != null)
                Debug.Log(e.Message);
        }
        catch (IOException e)
        {
            // Extract some information from this exception, and then
            // throw it to the parent method.
            if (e.Source != null)
                Debug.Log(e.Message);
            throw;
        }
        
    }
    public void UpdateHighscores(int highscore, string playerName) { //Update der HighscoreEinträge mit dem neuen Highscore ( Called in SaveHighscores())
        HighscoreEntry newvalue = new HighscoreEntry(highscore, playerName);
        HighscoreEntry _temp;
        for (int i=highscores.Length-1; i>=0;i--)
        {
            if (highscores[i] == null) {
                highscores[i] = newvalue;
                break;
            }
            else if (newvalue.score >= highscores[i].score) {
                _temp = highscores[i];
                highscores[i] = newvalue;
                newvalue = _temp;
            }
        }
    }
    public void SaveHighscores(string playerName) { //Speichern der Highscores in JSON Datei
        HighscoreData data = new HighscoreData();
        UpdateHighscores(highscore, playerName);
        data.entries = highscores;
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", jsonData);
        highscoreSaved = true;
    }

    public void DeleteHighscoreData() {
        foreach (HighscoreEntry entry in highscores) {
            entry.playerName = "n.a";
            entry.score = 0; 
        }
        HighscoreData data = new HighscoreData();
        data.entries = highscores;
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", jsonData);
        LoadHighscores();
    }
    
}
[System.Serializable]
public class HighscoreData //Hilfsklasse, die HighscoreEntry-s in JSON speichert.
{
    public HighscoreEntry[] entries = new HighscoreEntry[5];
}
[System.Serializable]
public class HighscoreEntry { //Hilfsklasse zur Speicherung von Highscores
    public int score;
    public string playerName;

    public HighscoreEntry(int s, string p) {
        score = s;
        playerName = p;
    }
}
