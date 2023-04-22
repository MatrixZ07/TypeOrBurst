public static class CurrentScoreHandler
{
    private static int currentScore = 0;
    public static int CurrentScore { get { return currentScore; } set { currentScore = value; } }

    public static void Reset() { currentScore = 0; }
    public static void AddToScore(int value)
    { 
        currentScore = currentScore + value * MultiplierHandler.Multiplier;
    }
}


