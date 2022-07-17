using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyType :MonoBehaviour
{
    public int maxEnemyTypes=4;
    //first inst is typeIndex of the enemy, second int is the probability of the enemy to spawn
    public List<KeyValuePair<int, int>> enemyTypes;

    public int[] enemyPercentageDistribution = new int[4];


    private int cumulative = 0;

    // Start is called before the first frame update
    private void Start()
    {
        enemyTypes = new List<KeyValuePair<int, int>>();
        for (int i = 0; i < enemyPercentageDistribution.Length; i++) {
            enemyTypes.Add(new KeyValuePair<int, int>(i+1, enemyPercentageDistribution[i]));
        }
    }
    /*
     * possibleEnemyTypes Input-Range: 1-4
     * maxEnemyTypes = 4    (normal, tank, fast, shadow)
     * Value (verteilung):    70%     10%   10%    10%
     * Key (selbst vergeben):  1       2     3     4
     * Position in List:       0       1     2     3
     */
    public int GetRandomEnemyType(int possibleEnemyTypes) {
        cumulative = 0;
        int randomInt = Random.Range(0, 101);
        for (int i = 0; i < possibleEnemyTypes; i++) {
            if (i == 0)
            {
                cumulative += AddNonSelectablesToValue(possibleEnemyTypes);
            }
            else { 
                cumulative += enemyTypes[i].Value;
            }
            if (randomInt <= cumulative) {
                int selectedEnemy = enemyTypes[i].Key;
                Debug.Log("selectedEnemyIndex : " + selectedEnemy + " ; Random Int was: " + randomInt+ " ;possibleEnemyTypes: "+possibleEnemyTypes+" enemyTypes[i].Key= "+ enemyTypes[i].Key);
                return selectedEnemy;
            }
        }
        return 0;
    }

    //Falls nicht alle EnemyTypes verfügbar sind (in ersten Waves), dann addiere die Wahrscheinlichkeit der anderen Enemytypes auf die Wahrscheinlichkeit des Standard-Enemies
    private int AddNonSelectablesToValue(int possibleEnemyTypes) {
        int percentage = enemyTypes[0].Value;
        if (possibleEnemyTypes < maxEnemyTypes)
        {
            for (int i = maxEnemyTypes - 1; i >= possibleEnemyTypes; i--) {
                percentage += enemyTypes[i].Value;
            }
        }
        Debug.Log("Returned percentage value for Type 1 : " + percentage);
        return percentage;
    }
}
