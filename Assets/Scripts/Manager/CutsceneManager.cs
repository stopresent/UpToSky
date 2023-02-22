using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    // 딕셔너리로 씬 저장
    public Dictionary<string, LoadSceneMode> loadScenes = new();

    // 컷신이 끝났는지 확인하는 변수
    public bool cutFinished = false;
    
    // 컷씬 순서
    public int SceneNumber = 0;

    // 딕셔너리에 컷씬 추가 (세 개로 가정)
    public void InitCutsceneInfo()
    {
        loadScenes.Clear();

        // additive란 현재 씬을 디스트로이 하지 않고 그 위에 씬 로드하는 방법
        loadScenes.Add("Cut#1", LoadSceneMode.Additive);
        loadScenes.Add("Cut#2", LoadSceneMode.Additive);
        loadScenes.Add("Cut#3", LoadSceneMode.Additive);
    }

    // 딕셔너리에 있는 컷씬을 load
    // 딕셔너리를 인덱스로 접근하기 위해 list로 변환하여 클릭할 때마다 스위칭 되게 함 
    public void PlayCutscene()
    {
        var _loadScene = Managers.Cutscene.loadScenes.ToList();
        SceneManager.LoadScene(_loadScene[SceneNumber].Key, _loadScene[SceneNumber].Value);
        SceneNumber++;
    }

    // 스택처럼 쌓인 컷씬을 모두 unload
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
