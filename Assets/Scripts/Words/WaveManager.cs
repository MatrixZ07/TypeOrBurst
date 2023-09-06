using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	//private HUDManager hudManager;

	//public int spawncount = 10;
	//public int waveIndex = 0; //WaveManager-Z�hler
	//Coroutine waveSpawn;
	//public int enemiesIncoming; //Verbleibende Enemies der momentanen Welle
	//private bool wavefinished;

	//private void Start()
	//{
	//	spawncount = 10;
	//	waveIndex = 1;
	//}
	//public void StartWave()
	//{
	//	if (!WaveCompleted())
	//	{
	//		return;
	//	}

		
	//	hudManager.ShowWaveDisplay(waveIndex);
	//	//Should not be done here. Should be done when game is started. 
	//	//if (waveIndex == 1)
	//	//{
	//	//	streak = 0;
	//	//	spawnTimer = 3f;
	//	//}
	//	spawncount += 10;
	//	enemiesIncoming = spawncount;
	//	words = new List<Word>();
	//	if (waveIndex < 2)
	//	{
	//		waveSpawn = StartCoroutine(AddWords(spawncount, 1));
	//	}
	//	else if (waveIndex < 3)
	//	{
	//		waveSpawn = StartCoroutine(AddWords(spawncount, 2));
	//	}
	//	else if (waveIndex < 4)
	//	{
	//		waveSpawn = StartCoroutine(AddWords(spawncount, 3));
	//	}
	//	else
	//	{
	//		waveSpawn = StartCoroutine(AddWords(spawncount, 4));
	//	}
	//}
	//public bool WaveCompleted()
	//{
	//	if (words.Count == 0 && enemiesIncoming == 0)
	//	{
	//		waveIndex++; 
	//		wavefinished = true;
	//		return wavefinished;
	//	}
	//	else
	//	{
	//		wavefinished = false;
	//		return wavefinished;
	//	}
	//}
	//public void StopWave()
	//{
	//	Reset();
	//	StopCoroutine(waveSpawn);
	//	wavefinished = true;
	//}
}
