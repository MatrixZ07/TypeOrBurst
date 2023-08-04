using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 
 
 */
public class WordManager : MonoBehaviour
{
    public List<Word> words;

    public RandomEnemyType randomEnemyType;
    public WordListManager wordListManager;
    public WordSpawner wordSpawner;
    public CurrentScoreDisplay currentScoreDisplay;

    private bool inputPossible=false;
    private bool hasActiveWord;
    private Word activeWord;
    [SerializeField] private float spawnTimer=3f;
    private int fiveToGo=0;
    [SerializeField] private float spawnTimeReduction = 0.05f;

    private int streak=0;
    private int streakCap = 10;

    public int spawncount=10;
    public int waveCount=0; //Wave-Z�hler
    Coroutine waveSpawn;
    public int enemiesIncoming; //Verbleibende Enemies der momentanen Welle
    private bool wavefinished;

    [SerializeField]
    private HUDManager hudManager;

    public UIManager uiManager;

    private void Start()
    {

    }
    // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX WAVEMANAGEMENT XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    /*WaveSpawn-Funktion : 
     * Jede Welle kommen 10 extra W�rter hinzu. 
     * Gegnertypen werden alle n Wellen hinzugef�gt.
     * Streak wird in Welle 1 zur�ckgesetzt, sonst beibehalten
     * verbleibende Enemies werden jede Welle gesetzt.
     */
    public void StartWave() {
        if (WaveCompleted()) {
            waveCount++;
            hudManager.ShowWaveDisplay(waveCount);
            if (waveCount == 1)
            {
                streak = 0;
                spawnTimer = 3f;
            }
            spawncount += 10;
            enemiesIncoming = spawncount;
            words = new List<Word>();
            if (waveCount < 2)
            {
                waveSpawn = StartCoroutine(AddWords(spawncount, 1));
            }
            else if (waveCount < 3)
            {
                waveSpawn = StartCoroutine(AddWords(spawncount, 2));
            }
            else if (waveCount < 4)
            {
                waveSpawn = StartCoroutine(AddWords(spawncount, 3));
            }
            else { 
                waveSpawn = StartCoroutine(AddWords(spawncount, 4));
            }
        }
    }
    public bool WaveCompleted() {
        if (words.Count == 0 && enemiesIncoming == 0) {
            wavefinished = true;
            return wavefinished;
        } else {
            wavefinished = false;
            return wavefinished; 
        }
    }
    public void StopWave()
    {
        RestoreDefaultValues();
        StopCoroutine(waveSpawn);
        wavefinished = true;
    }
    // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX ENEMY-GENERATION XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    //Single-Spawn eines Wortes
    public void AddWord(WordDisplay wordDisplay, int enemyType)
    {
        Word word = new Word(wordListManager.GetRandomWordArray(enemyType), wordDisplay);
        //Debug.Log(word.word);

        words.Add(word);
    }
    /* Adds wordCount Words to the Word-List based on the Index of possibleEnemyTypes
	 * Type Range: 1-4
	 * Type 1: Normal Enemy
	 * Type 2: Tank Enemy	--> string[2]
	 * Type 3: Fast Enemy
	 * Type 4: Shadow Enemy
	 */
    IEnumerator AddWords(int wordCount,int possibleEnemyTypes) //zeitverschobenes Spawnen einzelner Gegner(typen)
    {
        
        for (int i = 0; i < wordCount; i++)
        {
            int randomType = randomEnemyType.GetRandomEnemyType(possibleEnemyTypes);
            switch (randomType) {
                case 1: 
                    AddWord(wordSpawner.SpawnWord(WordSpawner.EnemyType.Normal), randomType); 
                    break;
                case 2:
                    AddWord(wordSpawner.SpawnWord(WordSpawner.EnemyType.Tank), randomType);
                    break;
                case 3:
                    AddWord(wordSpawner.SpawnWord(WordSpawner.EnemyType.Fast), randomType); 
                    break;
                case 4:
                    AddWord(wordSpawner.SpawnWord(WordSpawner.EnemyType.Shadow), randomType);
                    break;
                    // F�r zuk�nftiges Compound-word Hier AddCompound verwenden. --> AddCompount noch fertigstellen
                    //i++; //F�r Compounds i extra erh�hen, da zwei W�rter gespawned werden
            }
            yield return new WaitForSeconds(Random.Range(1f,spawnTimer));
        }
        yield return new WaitUntil(() => WaveCompleted());
        Debug.Log("Wave nummer " + waveCount + " besiegt! ");
        StartWave();
    }
    /*
    bool isWaveFinished() {
        return wavefinished;
    }*/
    //Spawnvariante: Compound-Word (Magnetic Enemy)
    /*public void AddCompound() //Bisher Ungenutzt
    {
        WordDisplay left= wordSpawner.SpawnWord();
        left.transform.localPosition = new Vector3(left.transform.localPosition.x * 0, left.transform.localPosition.y, 0f);
        left.transform.localPosition = new Vector3(left.transform.localPosition.x -180f, left.transform.localPosition.y, 0f);
        WordDisplay right = wordSpawner.SpawnWord();
        right.transform.localPosition = new Vector3(right.transform.localPosition.x * 0, left.transform.localPosition.y, 0f);
        right.transform.localPosition = new Vector3(right.transform.localPosition.x + 180f, left.transform.localPosition.y, 0f);
        AddWord(left,1);
        AddWord(right,1);
    }*/
    // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX HANDLING DES INPUTS/Der WORDLIST XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    public void SetInputPossible(bool value) {
        inputPossible = value;
    }
    //Wort mit dem Buchstaben der zuerst geschrieben wird wird aktives Wort
    public void TypeLetter(char letter)
    {
        if (inputPossible) {
            if (hasActiveWord)
            {
                //Check if letter was next
                //Remove it from the word
                if (activeWord.GetNextLetter() == letter)
                {
                    FindObjectOfType<AudioManager>().Play("Typing");
                    activeWord.TypeLetter();
                }
                else
                {
                    //Decrease Multiplier
                    FindObjectOfType<AudioManager>().Play("Mistyped");
                    MultiplierHandler.DecreaseMultiplier();
                    streak = 0;
                }
            }
            else
            {
                if (!WordWithLetterFound(letter))
                {
                    FindObjectOfType<AudioManager>().Play("Mistyped");
                    MultiplierHandler.DecreaseMultiplier();
					streak = 0;
				}
            }

            if (hasActiveWord && activeWord.WordTyped()) //setzt aktiveWord false
            {
                hasActiveWord = false;
                if (activeWord.CanDestroyWord())
                {
                    words.Remove(activeWord);
                    enemiesIncoming--;
                    Debug.Log(enemiesIncoming + " Enemies incoming.");
                    Debug.Log("Word Removed From List");
                }
                else {
                    Debug.Log("Word not removed from List, has Words left");
                }
                activeWord = null;
                Debug.Log("ActiveWord reset");
                if (streak >= streakCap)
                {
                    MultiplierHandler.IncreaseMultiplier();
                    streak = 0;
                }
                else
                {
                    streak++;
                }
                ReduceSpawnTime();
            }
        } 
    }
    public void RemoveDestroyedWord(WordDisplay wordDisplay) {
        if (activeWord != null)
        {
            if (activeWord.GetWordDisplay() == wordDisplay)
            {
                words.Remove(activeWord);
                hasActiveWord = false;
                Debug.Log("ActiveWord collided with Player and was removed.");
            }
        }
        foreach (Word word in words) {
            if (word.GetWordDisplay() == wordDisplay) {
                Debug.Log("Collision mit " + word.word + " erkannt. " + word.word + " beseitigt.");
                words.Remove(word);
                break;
            }
        }
        enemiesIncoming--;
    }
    /*public void DestroyedByCollision() {
        if (activeWord != null && activeWord.GetWordDisplay() == null)
        {
            //Falls das Wort durch Collision zerst�rt wurde bevor es fertig geschrieben wurde
            //Debug.Log("Display " + activeWord.GetWordDisplay().ToString() + activeWord.ToString());
            //Debug.Log(words.ToString());
            words.Remove(activeWord);
            Debug.Log("Komische Bedingung mit ActiveWord = " + activeWord.word);
            hasActiveWord = false;
        }
    }*/ 

    private bool WordWithLetterFound(char letter) {
        words = words.OrderBy(word => word.GetWordDisplay().transform.localPosition.y).ToList(); //Sortiert liste aufsteigend nach y-Positionen der WordDisplays der W�rter (n�hestes Wort zuerst)
        foreach (Word word in words)
        {
            if (word.GetNextLetter() == letter)
            {
                FindObjectOfType<AudioManager>().Play("Typing");
                activeWord = word;
                hasActiveWord = true;
                Debug.Log("ActiveWord is: " + activeWord.word);
                word.TypeLetter();
                return true;
            }
        }
        return false;
    }

    public void ReduceSpawnTime() {
        fiveToGo++;
        if (fiveToGo >= 5 && spawnTimer>0.5f)
        {
            spawnTimer -= spawnTimeReduction;
            fiveToGo = 0;
        }
    }

    private void RestoreDefaultValues() {
        fiveToGo = 0;
        streak = 0;
        spawncount = 10;
        waveCount = 0;
        enemiesIncoming = 0;
        wavefinished = true;
        words = new List<Word>();
    }
}
