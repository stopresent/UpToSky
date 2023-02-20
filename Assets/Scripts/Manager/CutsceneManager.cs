using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public Dictionary<string, LoadSceneMode> loadScenes = new();

    public void InitCutsceneInfo()
    {
        loadScenes.Add("Cut#1", LoadSceneMode.Additive);
        loadScenes.Add("Cut#2", LoadSceneMode.Additive);
        loadScenes.Add("Cut#3", LoadSceneMode.Additive);
    }

    void Start()
    {
        InitCutsceneInfo();

        foreach(var _loadScene in loadScenes)
        {
           LoadScene(_loadScene.Key, _loadScene.Value);
        }
    }
    
    public void LoadScene(string sceneName, LoadSceneMode mode)
    {
        if(Input.GetMouseButtonDown(0))
        {

            if (sceneName == "Cut#2")
                SceneManager.UnloadSceneAsync("Cut#1");
            else if (sceneName == "Cut#3")
                SceneManager.UnloadSceneAsync("Cut#2");
            else if (sceneName == "GameScene")
                SceneManager.UnloadSceneAsync("Cut#3");

            SceneManager.LoadScene(sceneName, mode);
        }
    }
}
