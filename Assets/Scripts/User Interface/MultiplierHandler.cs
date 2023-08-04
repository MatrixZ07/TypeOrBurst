using System;
using System.ComponentModel;

public static class MultiplierHandler
{
    private static int multiplier = 1;
    private static int multiplierCap = 10;
    public static int Multiplier 
    {
        get => multiplier;
        set 
        {
            multiplier = value;
			OnMultiplierChanged?.Invoke(multiplier);
		} 
    }
    public static event Action<int> OnMultiplierChanged;

	public static void Reset() { Multiplier = 1; }

    public static void IncreaseMultiplier() 
    {
        if (Multiplier < multiplierCap) Multiplier = Multiplier + 3;
    }
    public static void DecreaseMultiplier()
    { 
        if (Multiplier > 1) Multiplier = (Multiplier - 3 < 0) ? 0 : Multiplier - 3;
    }
}


