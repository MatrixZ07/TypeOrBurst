using UnityEngine;
using TMPro;

public class CurrentScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI multiplierText;

    void Start()
    {
        CurrentScoreHandler.Reset();
        MultiplierHandler.Reset();
        DisplayScore();
        DisplayMultiplier();
    }

    //TODO: Anzeigen von Punkten und Multiplier handlen. 
    public void DisplayScore()
    {
        currentScoreText.text = CurrentScoreHandler.CurrentScore.ToString();
    }

    public void DisplayMultiplier() { //Setzt Multiplier in GameUI
        multiplierText.text = MultiplierHandler.Multiplier.ToString() + "x";
    }

}


