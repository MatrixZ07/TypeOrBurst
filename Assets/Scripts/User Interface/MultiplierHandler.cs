public static class MultiplierHandler
{
    private static int multiplier = 1;
    private static int multiplierCap = 10;
    public static int Multiplier { get { return multiplier; } set { multiplier = value; } }
    public static void Reset() { multiplier = 1; }

    public static void IncreaseMultiplier() //Erhöht aktuellen Multiplier
    {
        if (multiplier < multiplierCap) multiplier = multiplier + 3;
    }
    public static void DecreaseMultiplier()
    { //Senkt aktuellen Multiplier
        if (multiplier > 1) multiplier = (multiplier - 3 < 0) ? 0 : multiplier - 3;
    }
}


