using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_Game : UI_Scene
{
    enum Buttons
    {
        SettingBtn,
    }

    enum Texts
    {
        ScoreText,
    }

    enum Images
    {
        ScoreImage,
    }

    enum GameObjects
    {

    }


    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // TODO 배경에 따라 브금이 바뀌어야 함
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_Lazy", Sound.Bgm);

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.SettingBtn).gameObject.BindEvent(Setting);

        return true;
    }

    void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
