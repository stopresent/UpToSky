using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class UI_Main : UI_Scene
{
    enum Buttons
    {
        GameStartBtn,
        QuitGameBtn,
        ExplanBtn,
        CollectionBtn,
        DeveloperBtn,
    }

    enum Texts
    {
        GameStartText,
        QuitGameText,
        ExplanText,
        DeveloperText,
        MaxScoreText,
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

        GetButton((int)Buttons.GameStartBtn).gameObject.BindEvent(ToGameScene);
        GetButton((int)Buttons.QuitGameBtn).gameObject.BindEvent(QuitGame);
        GetButton((int)Buttons.ExplanBtn).gameObject.BindEvent(ExplanGame);
        GetButton((int)Buttons.CollectionBtn).gameObject.BindEvent(Collection);
        GetButton((int)Buttons.DeveloperBtn).gameObject.BindEvent(ShowDeveloper);

        // 메인 브금이 있으면 추가
        // TODO
        // Managers.Sound.Play("");

        string scoreText;
        if (PlayerPrefs.HasKey("highestScore"))
            scoreText = String.Format("{0:#,###} m", $"{PlayerPrefs.GetInt("highestScore")}");
        else
            scoreText = "0 m";
        GetText((int)Texts.MaxScoreText).text = scoreText;
        return true;
    }

    void ToGameScene()
    {
        // 게임 시작
        Debug.Log("게임 시작!");

        Managers.UI.ShowPopupUI<UI_SelectMode>();

        // UI 클릭 사운드
        Managers.Sound.Play("Sound_OpenUI");

    }

    void QuitGame()
    {
        //  게임 나가기!
        Debug.Log("게임 나가기!");
        Application.Quit();
    }

    void ExplanGame()
    {
        // TODO
        // 게임 설명 팝업 띄우기
        Debug.Log("이 게임은 이런 게임!");
        Managers.UI.ShowPopupUI<UI_HowToGame>();
        Managers.Sound.Play("Sound_OpenUI");



    }

    void Collection()
    {
        Debug.Log("콜렉션 창");
        Managers.UI.ShowPopupUI<UI_Collection>();
        Managers.Sound.Play("Sound_OpenUI");
    }

    void ShowDeveloper()
    {
        // TODO
        // 개발자 소개..
        Debug.Log("하소연 프로젝트..!");
        Managers.UI.ShowPopupUI<UI_ShowDeveloper>();
        Managers.Sound.Play("Sound_OpenUI");

    }
}
