using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    public int highestScore;
    public int Score;

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

    private void Update()
    {
        Score = (int)GameObject.Find("Player").transform.position.y;
        if(highestScore < Score)
            highestScore = Score;

        RefreshUI();
    }

    void RefreshUI()
    {
        GetText((int)Texts.ScoreText).text = String.Format("{0:#,###} m", $"{Score}");
    }

    void Setting()
    {
        // TODO
        // setting 창 열고 시간 정지
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
