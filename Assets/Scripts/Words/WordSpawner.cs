using UnityEngine;
using System;


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
        GameObject enemyType = GetEnemyPrefab(type);

        GameObject wordObj = Instantiate(enemyType, enemyType.transform.position, Quaternion.identity, wordCanvas);
        wordObj.transform.localPosition = new Vector3(wordObj.transform.localPosition.x + UnityEngine.Random.Range(-160f, 160f), wordObj.transform.position.y, wordObj.transform.position.z);

        return wordObj.GetComponentInChildren<WordDisplay>();
    }

    private GameObject GetEnemyPrefab(EnemyType type)
    {
		switch (type)
		{
			case EnemyType.Normal:
				return normalEnemyPrefab;
			case EnemyType.Fast:
				return fastEnemyPrefab;
			case EnemyType.Tank:
				return tankEnemyPrefab;
			case EnemyType.Shadow:
				return shadowEnemyPrefab;
			default:
				throw new ArgumentException("EnemyType of type " + type.ToString() +" does not exist.");
		}
	}

}
