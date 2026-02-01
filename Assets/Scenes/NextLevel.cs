using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public ScriptScene scriptScene;

    public void LevelSelanjutnya(string key)
    {
        // Level Selanjutnya ke Unlock
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
        scriptScene.PindahScene("Main Level");
    }
}
