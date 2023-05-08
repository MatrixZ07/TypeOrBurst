using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class WordListManager : MonoBehaviour
{
	public WordManager wordManager;

	public SettingsMenu settingsMenu;

	public static bool useCustomWordList = false;
	public static bool hasCustomWordList = false;

	
	public TMP_InputField customWordInput;
	static List<string> customWordList = new List<string>();

	static List<string> defaultWordList = new List<string>();
	public TextAsset defaultWordsTXT;

	//die derzeit aktive Wortliste
	static List<string> activeWordList = new List<string>();

	//Variable zur Speicherung der Dateipfade
	private static string defaultWordListPath;
	private static string customWordListPath ;

    private void Awake()
    {
		defaultWordListPath = Application.persistentDataPath + "/defaultWordList.json";
		customWordListPath = Application.persistentDataPath + "/customWordList.json";

	}

    private void Start()
    {
		settingsMenu.LoadPlayerPrefs();
		if (File.Exists(customWordListPath))
		{
			Debug.Log("Custom Word List Path does exist!");
			customWordList = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(customWordListPath));
			//customWordList = JsonUtility.FromJson<List<string>>(File.ReadAllText(customWordListPath)).Distinct().ToList();
		}
		if (File.Exists(defaultWordListPath)) { 
			Debug.Log("Default Word List Path does exist!");
			//defaultWordList = JsonUtility.FromJson<List<string>>(File.ReadAllText(defaultWordListPath)).Distinct().ToList();
			defaultWordList = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(defaultWordListPath));

		}
		string[] defaultWordArray = defaultWordsTXT.text.Split(' ');
		if (defaultWordArray != defaultWordList.ToArray()) {
			defaultWordList = new List<string>();
			foreach (string word in defaultWordArray)
			{
				defaultWordList.Add(word);
			}
			//string jsonData = JsonUtility.ToJson(defaultWordList, true);
			//File.WriteAllText(defaultWordListPath, jsonData);
			string jsonData = JsonConvert.SerializeObject(defaultWordList);
			File.WriteAllText(defaultWordListPath, jsonData);
		}
		activeWordList = (hasCustomWordList && useCustomWordList && customWordList.Count>0) ? customWordList : defaultWordList;
		/**/
		Debug.Log("Contents of Active Word Lists after Start() are: ");
		foreach (string word in activeWordList) Debug.Log(word.ToString());
		/**/
    }

    public void CreateCustomWordList() { //Aufgerufen über den Save Button in den Settings
		customWordList = new List<string>();
		string trimmed = System.String.Concat(customWordInput.text.Where(c => !System.Char.IsWhiteSpace(c)));
		Debug.Log("Trimmed customWordList is : " + trimmed);
		if (trimmed != "" && trimmed.Length>0) { 
			string [] wordarray = customWordInput.text.Split(' ');
			foreach (string word in wordarray) {
				customWordList.Add(word);
			}
			customWordList = customWordList.Distinct().ToList();
        }
		else{
			Debug.Log("Creating CustomWordList failed. Check Input. Make sure to separate words with a space character");
		}
	}

	public void SaveCustomWordListToJSON() //Aufgerufen über den Save Button in den Settings
	{ 
		Debug.Log(customWordList.Count.ToString());
		if (customWordList.Count > 0)
		{

			//string jsonData = JsonUtility.ToJson(customWordList, true);
			string jsonData = JsonConvert.SerializeObject(customWordList);
			File.WriteAllText(customWordListPath, jsonData);

			hasCustomWordList = true;
			PlayerPrefs.SetInt("hasCustomWordList", System.Convert.ToInt32(hasCustomWordList));
			PlayerPrefs.Save();
			settingsMenu.StartCoroutine("DisplaySaveText");
		}
		else {
			hasCustomWordList = false;
			PlayerPrefs.SetInt("hasCustomWordList", System.Convert.ToInt32(hasCustomWordList));
			PlayerPrefs.Save();
		}
	}

	public void LoadCustomWordsFromJSON() //Bei Spielstart
	{ //Laden der Wordlist aus der JSON datei
		try
		{
			customWordList = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(customWordListPath));

			//customWordList = JsonUtility.FromJson<List<string>>(File.ReadAllText(customWordListPath));
		}
		catch (FileNotFoundException e)
		{
			// FileNotFoundExceptions are handled here.
			PlayerPrefs.SetInt("hasCustomWordList", 0);
			PlayerPrefs.Save();
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


	public string GetRandomWord()
    {
		activeWordList = (hasCustomWordList && useCustomWordList) ? customWordList : defaultWordList;
		string randomWord = GetUniqueWord();
		return randomWord;
    }

	//returns a Word, whiches first letter is not used in the current List of Words of WordManager
	public string GetUniqueWord() {
		int randomIndex = Random.Range(0, activeWordList.Count);
		bool isUsed = false;
		if (wordManager.words.Count > 0) { 
			foreach (Word word in wordManager.words) {
				if (word.word[0] == activeWordList[randomIndex][0]) isUsed = true;
				break;
			}
		}
		return (isUsed)? GetUniqueWord() : activeWordList[randomIndex];
	}
	/*Returns an array whiches size is based on the enemyTypeIndex
	 * Type Range: 1-4
	 * Type 1: Normal Enemy
	 * Type 2: Tank Enemy	--> string[2] (2 Wörter)
	 * Type 3: Fast Enemy
	 * Type 4: Shadow Enemy
	 */
	public string[] GetRandomWordArray(int enemyTypeIndex) {
		string[] wordArray;
		if (enemyTypeIndex == 2) //für "TANK"-Enemy
		{
			//Return "TANK"-Enemy
			wordArray = new string[2];
			for (int i = 0; i < wordArray.Length; i++)
			{
				wordArray[i] = GetRandomWord();
			}
		}
		else {
			wordArray = new string[1];
			wordArray[0] = GetRandomWord();
		}
		return wordArray;
	}

}
