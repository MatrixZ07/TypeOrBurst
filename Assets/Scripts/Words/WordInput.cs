using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gibt aktuellen Input in der Konsole aus

public class WordInput : MonoBehaviour
{
    public WordManager wordManager;

    // Update is called once per frame
    void Update()
    {
        foreach (char letter in Input.inputString)
        {
            wordManager.TypeLetter(letter);
            //Debug.Log(letter);
        }
    }
}
