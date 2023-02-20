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

        // ���� ����� ������ �߰�
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
        // ���� ����
        Debug.Log("���� ����!");

        Managers.UI.ShowPopupUI<UI_SelectMode>();

        // UI Ŭ�� ����
        Managers.Sound.Play("Sound_OpenUI");

    }

    void QuitGame()
    {
        //  ���� ������!
        Debug.Log("���� ������!");
        Application.Quit();
    }

    void ExplanGame()
    {
        // TODO
        // ���� ���� �˾� ����
        Debug.Log("�� ������ �̷� ����!");
        Managers.UI.ShowPopupUI<UI_HowToGame>();
        Managers.Sound.Play("Sound_OpenUI");



    }

    void Collection()
    {
        Debug.Log("�ݷ��� â");
        Managers.UI.ShowPopupUI<UI_Collection>();
        Managers.Sound.Play("Sound_OpenUI");
    }

    void ShowDeveloper()
    {
        // TODO
        // ������ �Ұ�..
        Debug.Log("�ϼҿ� ������Ʈ..!");
        Managers.UI.ShowPopupUI<UI_ShowDeveloper>();
        Managers.Sound.Play("Sound_OpenUI");

    }
}
