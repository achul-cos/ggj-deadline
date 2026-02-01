using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    public Button buttonLevels2;
    public Button buttonLevels3;
    public ScriptScene scriptScene;

    void Start()
    {
        PersiapanSave();
        CheckLevel();
    }

    public void CheckLevel()
    {
        int statusLevel2 = PlayerPrefs.GetInt("Level 2");
        int statusLevel3 = PlayerPrefs.GetInt("Level 3");

        if(buttonLevels2 != null)
        {
            buttonLevels2.interactable = (statusLevel2 == 1);
        }

        if(buttonLevels3 != null)
        {
            buttonLevels3.interactable = (statusLevel3 == 1);
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        // CheckLevel();

        // if(scriptScene != null)
        // {
        //     scriptScene.PindahScene("Main Level");
        // }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Level");
    }

    public void PersiapanSave()
    {
        if(PlayerPrefs.HasKey("Level 2") == false)
        {
            PlayerPrefs.SetInt("Level 2", 0);
            PlayerPrefs.SetInt("Level 3", 0);
            PlayerPrefs.Save();
        }
    }

    // Level 2
    // 1 == unlock
    // 0 == Lock
    // Level 3
}
