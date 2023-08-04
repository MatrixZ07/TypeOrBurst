using System.ComponentModel;
using System;

public static class CurrentScoreHandler
{
    private static int currentScore = 0;
    public static int CurrentScore 
    { 
        get => currentScore; 
        set 
        { 
            currentScore = value;
            OnScoreChanged?.Invoke(currentScore);
            //OnPropertyChanged(nameof(CurrentScore));
        } 
    }
    public static event Action<int> OnScoreChanged;

	public static void Reset() { CurrentScore = 0; }
    public static void AddToScore(int value)
    { 
        CurrentScore = CurrentScore + value * MultiplierHandler.Multiplier;
    }
}


