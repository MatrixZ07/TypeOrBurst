using UnityEngine;
using TMPro;
using System.ComponentModel;

public class CurrentScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI multiplierText;

    void Start()
    {
        CurrentScoreHandler.Reset();
        MultiplierHandler.Reset();
        CurrentScoreHandler.OnScoreChanged += DisplayScore;
        MultiplierHandler.OnMultiplierChanged += DisplayMultiplier;
        //DisplayScore();
        //DisplayMultiplier();
    }

    //TODO: Anzeigen von Punkten und Multiplier handlen. 
    public void DisplayScore(int newScore)
    {
        currentScoreText.text = newScore.ToString();
    }

    public void DisplayMultiplier(int newMultiplier) { 
        multiplierText.text = MultiplierHandler.Multiplier.ToString() + "x";
    }

}


