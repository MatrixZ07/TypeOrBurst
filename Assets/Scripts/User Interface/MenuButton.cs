using UnityEngine;

public class MenuButton : MonoBehaviour, IDisplay
{
    public void OpenMenu(GameObject menuToDisplay)
    {
        UIManager.Instance.CloseCurrentMenu();
        UIManager.Instance.CurrentOpenMenu = menuToDisplay.gameObject;
        UIManager.Instance.CurrentOpenMenu.SetActive(true);
    }
    
}

