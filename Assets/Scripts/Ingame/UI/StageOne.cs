using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageOne : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject completeMenu;
    public GameObject loseMenu;


    public void OnClickOpenSettingMenu()
    {
        Debug.Log("설정");
        settingMenu.SetActive(true);
        if(settingMenu.activeSelf == true)
        {
            Debug.Log("멈춤");
            Time.timeScale = 0f;
        }

    }
    public void OnClickCloseSettingMenu()
    {
        Debug.Log("닫기");
        settingMenu.SetActive(false);
        if (settingMenu.activeSelf == false)
        {
            Debug.Log("고고");
            Time.timeScale = 1f;
        }
    }

    public void OnClickGameReset()
    {
        Debug.Log("다시하기");
        Time.timeScale = 1f;
        settingMenu.SetActive(false);
        SceneManager.LoadScene("Stage1");

    }

    public void OnClickGameReturn()
    {
        Debug.Log("스테이지 선택");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage Menu");

    }

    public void OnClickCloseComplete()
    {
        completeMenu.SetActive(false);
        Time.timeScale = 0f;
    }
   
    public void OnClickCloseLose()
    {
        loseMenu.SetActive(false);
        Time.timeScale = 0f;
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
