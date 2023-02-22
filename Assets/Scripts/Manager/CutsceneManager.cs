using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    // ��ųʸ��� �� ����
    public Dictionary<string, LoadSceneMode> loadScenes = new();

    // �ƽ��� �������� Ȯ���ϴ� ����
    public bool cutFinished = false;
    
    // �ƾ� ����
    public int SceneNumber = 0;

    // ��ųʸ��� �ƾ� �߰� (�� ���� ����)
    public void InitCutsceneInfo()
    {
        loadScenes.Clear();

        // additive�� ���� ���� ��Ʈ���� ���� �ʰ� �� ���� �� �ε��ϴ� ���
        loadScenes.Add("Cut#1", LoadSceneMode.Additive);
        loadScenes.Add("Cut#2", LoadSceneMode.Additive);
        loadScenes.Add("Cut#3", LoadSceneMode.Additive);
    }

    // ��ųʸ��� �ִ� �ƾ��� load
    // ��ųʸ��� �ε����� �����ϱ� ���� list�� ��ȯ�Ͽ� Ŭ���� ������ ����Ī �ǰ� �� 
    public void PlayCutscene()
    {
        var _loadScene = Managers.Cutscene.loadScenes.ToList();
        SceneManager.LoadScene(_loadScene[SceneNumber].Key, _loadScene[SceneNumber].Value);
        SceneNumber++;
    }

    // ����ó�� ���� �ƾ��� ��� unload
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
