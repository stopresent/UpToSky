using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.SettingBtn).gameObject.BindEvent(Setting);

        return true;
    }

    void Setting()
    {
        // TODO
        // setting 창 열고 시간 정지
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
