using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wörter werden nicht relativ zum Canvas gespawned, dadurch verschiebung der Koordinaten
//Instanziiert Wörter

public class WordSpawner : MonoBehaviour
{
    public enum EnemyType  {Normal,Fast,Tank,Shadow};

    public GameObject normalEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject tankEnemyPrefab;
    public GameObject shadowEnemyPrefab;

    public Transform wordCanvas;

    public WordDisplay SpawnWord(EnemyType type) //Called in WordManager AddWord()
    {
        GameObject enemyType=null;
        switch (type) {
            case EnemyType.Normal:
                enemyType = normalEnemyPrefab;
                break;
            case EnemyType.Fast:
                enemyType = fastEnemyPrefab;
                break;
            case EnemyType.Tank:
                enemyType = tankEnemyPrefab;
                break;
            case EnemyType.Shadow:
                enemyType = shadowEnemyPrefab;
                break;
            default:break;
        }
        GameObject wordObj;
        if (enemyType != null) {
            wordObj = Instantiate(enemyType, enemyType.transform.position, Quaternion.identity, wordCanvas);
            wordObj.transform.localPosition = new Vector3(wordObj.transform.localPosition.x + Random.Range(-160f, 160f), wordObj.transform.position.y, wordObj.transform.position.z);
            WordDisplay wordDisplay = wordObj.GetComponentInChildren<WordDisplay>();
            return wordDisplay;
        }
        return null;
    }

}
