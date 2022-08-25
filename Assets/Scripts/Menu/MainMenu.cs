using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject storyMenu;
    public void OnClickStartGame()
    {
        Debug.Log("∞‘¿” Ω√¿€");
        storyMenu.SetActive(true);
    }
    public void OnClickSkip()
    {
        Debug.Log("Ω∫≈µ");
       
        AudioManager.instance.GoToStageMenu();
    }
    public void OnClickOpenSettingMenu()
    {
        Debug.Log("º≥¡§");
        settingMenu.SetActive(true);

    }
    public void OnClickCloseSettingMenu()
    {
        Debug.Log("¥›±‚");
        settingMenu.SetActive(false);
    }
    public void OnClickQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}