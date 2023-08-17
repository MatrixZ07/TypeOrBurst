using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public sealed class UIManager : MonoBehaviour
{
    // 2023 CODE
    #region Implementation of Singleton Pattern
    private static UIManager _instance;
    private static readonly object padlock = new object();

    UIManager() { }
    public static UIManager Instance 
    { get
        {
            lock (padlock)
            {
                if (_instance != null)
                {
                    return _instance; 
                }

                Debug.LogError("UIManager not instantiated!");
                return null;
            }
        }
        private set { _instance = value; }
    }
    #endregion

    [SerializeField]
    private static GameObject _currentOpenMenu;

    public GameObject CurrentOpenMenu { 
        get { return _currentOpenMenu; } 
        set 
        {
            if (_currentOpenMenu == value)
                return;
            CloseCurrentMenu();
            _currentOpenMenu = value;
            _currentOpenMenu.SetActive(true);
            Console.WriteLine($"Active Menu is: {_currentOpenMenu.name}.");
        } 
    }

    public static void OnOpenMenuChanged()
    {

    }
    public void CloseCurrentMenu()
    {
        if (_currentOpenMenu != null)
        {
            Console.WriteLine($"Trying to close Menu: {_currentOpenMenu.name}");
            Instance.CurrentOpenMenu.SetActive(false);
        }
    }

    private void Awake()
    {
        Instance = this;
        Debug.Log($"UIManager Instance set to {this.gameObject.name} ");
        _currentOpenMenu = GameObject.Find("MainMenu").gameObject;
        _currentOpenMenu.SetActive(true);
    }
    public void Start()
    {
        
        
        if (_currentOpenMenu == null) { Console.WriteLine("_currentOpenMenu was not set."); }
        
    }
    // LEGACY CODE
    public PlayerManager playerManager; //Für Sprite?

    //[SerializeField] public GameObject mainmenu;
    //[SerializeField] public GameObject highscoresMenu;
    //[SerializeField] public GameObject settingsMenu;
    
    //public GameObject highscoreContainerPrefab;
    //private GameObject highscoresMenuBoard;

    //public void SetState(UIState state)
    //{
    //    _currentState = state;
    //}
    
    //public void ShowMainMenu(bool show)
    //{
    //    //TODO: Anzeige der Menüs überarbeiten
    //    if (show)
    //    {
    //        mainmenu.SetActive(true);
    //        ShowHighscores(false);
    //        ShowSettings(false);
    //    }
    //    else {
    //        mainmenu.SetActive(false); 
    //    }
        
    //}
    
    //public void ShowSettings(bool show) {
    //    if (show)
    //    {
    //        settingsMenu.SetActive(true);
    //        ShowMainMenu(false);
    //    }
    //    else {
    //        if(settingsMenu != null)
    //            settingsMenu.SetActive(false);
    //    }
    //}
    
    //public void ShowHighscores(bool show) //HIGHSCOREANZEIGE VOM HAUPTMEN�
    //{
    //    if (show)
    //    {
    //        highscoresMenu.SetActive(true);
    //        ShowMainMenu(false);
    //        if (GameObject.Find("HighscoresMenu") != null) {
    //            highscoresMenuBoard = Instantiate(highscoreContainerPrefab,
    //                highscoreContainerPrefab.transform.localPosition, Quaternion.identity,
    //                highscoresMenu.transform) as GameObject;
    //            var localPosition = highscoresMenuBoard.transform.localPosition;
    //            localPosition = new Vector3(localPosition.x,
    //                localPosition.y - 50f, localPosition.z);
    //            highscoresMenuBoard.transform.localPosition = localPosition;
    //            highscoresMenuBoard.GetComponent<HighscoreBoard>()
    //                .ShowInputContainer(false); //�ber ShowInputContainer(false) wird auch UpdateDisplay() aufgerufen und die Anzeige mit den Highscores gef�ttert.
    //        }
    //    }
    //    else
    //    { 
    //        if(highscoresMenuBoard != null)
    //            DestroyImmediate(highscoresMenuBoard);
    //        if(highscoresMenu != null)
    //            highscoresMenu.SetActive(false);
    //    }
    //}
}

//public abstract class UIState
//{
//    protected virtual void ShowMainMenu() { }
//    protected virtual void ShowHighscoreMenu() { }
//    protected virtual void ShowSettingsMenu() { }
//}

//public class MainMenuState : UIState
//{
//    protected override void ShowMainMenu() { }
//}
//public class HighscoreMenuState : UIState
//{
//    protected override void ShowMainMenu() { }
//}
//public class SettingsMenuState : UIState 
//{
//    protected override void ShowMainMenu() { }
//}

