using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PlayMenu playMenu;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowPlayMenu()
    {
        mainMenu.gameObject.SetActive(false);
        playMenu.gameObject.SetActive(true);
    }

    public void ShowMainMenu()
    {
        playMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void PlayButtonClickSFX()
    {
        if(AudioManager.Global != null)
        {
            AudioManager.Global.Play("ButtonClick");
        }
    }
}
