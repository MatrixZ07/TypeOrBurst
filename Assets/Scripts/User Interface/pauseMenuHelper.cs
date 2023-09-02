using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuHelper : MonoBehaviour
{
    HUDManager hudManager;

    private void Awake()
    {
        hudManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDManager>();
    }
    public void ResumeGame() {
		hudManager.ResumeGame();
    }
    public void ShowMainMenu()
    {
        GameManager.LoadMainMenuScene();
    }
}
