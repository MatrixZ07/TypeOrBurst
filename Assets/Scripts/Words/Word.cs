using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public HighscoreManager highscore;
    public string word;

    //für Tank
    [SerializeField]
    private string[] wordArray;
    private int arrayIndex; //gibt den Index des aktiven Wortes an. Entspricht dieser wordArray.Length-1 wurde das letzte Wort dieses Wortes erreicht.

    private int typeIndex; //aktives Zeichen

    private WordDisplay display;

    private int value=0;

    /*public Word(string _word, WordDisplay _display)
    {
        highscore = GameObject.FindGameObjectWithTag("HighscoreManager").GetComponent <HighscoreManager> ();

        word = _word;
        foreach(char c in word)     //Wortpunktzahl = Wortlänge*10punkte
        {
            value += 10;
        }
        if (word.Length >= 7)       //Extrapunkte für längere Wörter. Ab 7 characters 1.5x, ab 11 characters 3.0x Punkte
        {
            value = (word.Length > 10) ? value * 3 : Mathf.RoundToInt(value * 1.5f);
        }
        typeIndex = 0;
        display = _display;
        display.SetWord(word);
    }*/

    //Konstruktor für TANK-Word
    public Word(string[] _word, WordDisplay _display) {
        highscore = GameObject.FindGameObjectWithTag("HighscoreManager").GetComponent<HighscoreManager>();

        wordArray = _word;
        word = wordArray[0];
        //Wortpunktzahl = Wortlänge*10*Wortlängen-Multiplikator
        value += word.Length*10;
        if (word.Length >= 7)       //Extrapunkte für längere Wörter. Ab 7 characters 1.5x, ab 11 characters 3.0x Punkte
        {
            value = (word.Length > 10) ? value * 3 : Mathf.RoundToInt(value * 1.5f);
        }
        typeIndex = 0;
        display = _display;
        display.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void TypeLetter()
    {
        typeIndex++;
        display.RemoveLetter();
    }
    public WordDisplay GetWordDisplay() {
        return display;
    }

    /*WordTyped Rückgabewert auf String ändern für Tankgegner 
     * ---> in WordManager behandeln, ob "null" zurückgegeben wird 
     * -> Word vollständig geschrieben, oder wenn ein neuer String zurückgegeben wird, den String als aktivenString "word" setzen und das Wort noch nicht removen/Zerstören
    */
    //bei TypeWord in WordManager muss ein Word zurückgegeben werden können.
    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped)
        {
            typeIndex = 0;
            highscore.Add(value);
        }
        return wordTyped;
    }
    public bool CanDestroyWord() //Übergibt dem WordManager, ob das Wort aus der Liste gelöscht werden kann
    {
        if (CanLoadNextWord())
        {
            return false;
        }
        else
        {
            display.RemoveWord();
            return true;
        }
    }
    public bool CanLoadNextWord() //Aufgerufen in CanDestroyWord() von WordManager 
    {
        if (arrayIndex < wordArray.Length - 1)
        {
            arrayIndex++;
            word = wordArray[arrayIndex];
            display.SetWord(wordArray[arrayIndex]);
            display.ResetColor();
            return true;
        }
        else { 
            return false;
        }
    }

}
