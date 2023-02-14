using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Game>();
        Debug.Log("Init");

        GameObject player = Managers.Resource.Instantiate("Player");
        player.transform.position = GameObject.Find("StartPoint").transform.position;
        return true;
    }
}
