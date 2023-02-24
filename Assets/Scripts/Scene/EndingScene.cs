using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Ending;
        Managers.UI.ShowSceneUI<UI_Ending>();
        Debug.Log("UI_Ending Init");

        return true;
    }
}