using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public Dictionary<string, LoadSceneMode> loadScenes = new();
    public bool cutFinished = false;
    public int SceneNumber = 0;

    public void InitCutsceneInfo()
    {
        loadScenes.Clear();
        loadScenes.Add("Cut#1", LoadSceneMode.Additive);
        loadScenes.Add("Cut#2", LoadSceneMode.Additive);
        loadScenes.Add("Cut#3", LoadSceneMode.Additive);
    }

    public void LoadCutScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
    }

    public void PlayCutscene()
    {
        var _loadScene = Managers.Cutscene.loadScenes.ToList();
        Managers.Cutscene.LoadCutScene(_loadScene[SceneNumber].Key, _loadScene[SceneNumber].Value);
        SceneNumber++;
    }
    public void DistroyCutscene()
    {
        cutFinished= true;
        SceneManager.UnloadSceneAsync("Cut#1");
        SceneManager.UnloadSceneAsync("Cut#2");
        SceneManager.UnloadSceneAsync("Cut#3");
        SceneNumber = 0;
        return;
    }
}
