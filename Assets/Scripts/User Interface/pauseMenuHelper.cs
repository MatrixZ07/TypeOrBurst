using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuHelper : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void ResumeGame() {
        gameManager.ResumeGame();
    }
    //public void ShowMainMenu() {
    //    gameManager.uiManager.ShowMainMenu(true);
    //}
    public void EndGame()
    {
        gameManager.EndGame();
    }
    public void ExitApplication() {
        gameManager.ExitApplication();
    }
}
