using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public enum MenuType
    {
        MainMenu,
        SettingsMenu,
        HighscoreMenu,
    }

    public PlayerManager playerManager; //Für Sprite?

    [SerializeField] public GameObject mainmenu;
    [SerializeField] public GameObject highscoresMenu;
    [SerializeField] public GameObject settingsMenu;
    
    public GameObject highscoreContainerPrefab;
    public GameObject highscoresMenuBoard;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMenu(MenuType type)
    {
        switch (type)
        {
            case(MenuType.MainMenu):
                break;
            case(MenuType.SettingsMenu):
                break;
            case(MenuType.HighscoreMenu):
                break;
            default:
                Debug.Log("No valid MenuType submitted.");
                break;
        }
    }
    
    
    public void ShowMainMenu(bool show)
    {
        //TODO: Anzeige der Menüs überarbeiten
        if (show)
        {
            mainmenu.SetActive(true);
            ShowHighscores(false);
            ShowSettings(false);
        }
        else {
            mainmenu.SetActive(false); 
        }
        
    }
    
    public void ShowSettings(bool show) {
        if (show)
        {
            settingsMenu.SetActive(true);
            ShowMainMenu(false);
        }
        else {
            if(settingsMenu != null)
                settingsMenu.SetActive(false);
        }
    }
    
    public void ShowHighscores(bool show) //HIGHSCOREANZEIGE VOM HAUPTMEN�
    {
        if (show)
        {
            highscoresMenu.SetActive(true);
            ShowMainMenu(false);
            if (GameObject.Find("HighscoresMenu") != null) {
                highscoresMenuBoard = Instantiate(highscoreContainerPrefab,
                    highscoreContainerPrefab.transform.localPosition, Quaternion.identity,
                    highscoresMenu.transform) as GameObject;
                var localPosition = highscoresMenuBoard.transform.localPosition;
                localPosition = new Vector3(localPosition.x,
                    localPosition.y - 50f, localPosition.z);
                highscoresMenuBoard.transform.localPosition = localPosition;
                highscoresMenuBoard.GetComponent<HighscoreBoardLoader>()
                    .ShowInputContainer(false); //�ber ShowInputContainer(false) wird auch DisplayHighscores() aufgerufen und die Anzeige mit den Highscores gef�ttert.
            }
        }
        else
        { 
            if(highscoresMenuBoard != null)
                Destroy(highscoresMenuBoard);
            if(highscoresMenu != null)
                highscoresMenu.SetActive(false);
        }
    }
}
