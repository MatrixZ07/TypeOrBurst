using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;

public class TextManager : MonoBehaviour
{

    //KANN GELÖSCHT WERDEN

    private TextMeshProUGUI activeText;
    public TextAsset textfile;

    // Start is called before the first frame update
    void Start()
    {
        activeText = GetComponent<TextMeshProUGUI>();
        LoadText(textfile);
    }

    // Update is called once per frame
    void Update()
    {
        //Consoleninput gegen activeText checken
        for (int i = 0; i < activeText.text.Length; i++)
        {
            if (CheckChar(Console.ReadKey().KeyChar, activeText.text[i]))
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("false");
            }
        }
    }

    void LoadText(TextAsset textAsset)
    {
        activeText.text = textAsset.text;
    }

    public bool CheckChar(char a, char b)
    {
        return (a == b) ? true : false;
    }
}
