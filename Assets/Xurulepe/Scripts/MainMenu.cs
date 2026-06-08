using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<Menu> menuList = new List<Menu>();

    private int currentMenuIndex = 0;

    public void OpenMenu(Menu menu)
    {
        if (!menuList.Contains(menu))
        {
            Debug.LogWarning("Invalid menu: " + menu.gameObject.name);
            return;
        }

        CloseCurrentMenu();

        menu.Open();

        currentMenuIndex = menuList.IndexOf(menu);
    }

    private void CloseCurrentMenu()
    {
        if (currentMenuIndex >= 0 && currentMenuIndex < menuList.Count)
        {
            menuList[currentMenuIndex].Close();
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
