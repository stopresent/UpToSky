using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UI_Main : UI_Scene
{
    enum Buttons
    {
        GameStart,
        QuitGame,
        Explan,
        Developer,
    }

    enum Texts
    {
        GameStartText,
        QuitGameText,
        ExplanText,
        DeveloperText,
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

        GetButton((int)Buttons.GameStart).gameObject.BindEvent(ToGameScene);
        GetButton((int)Buttons.QuitGame).gameObject.BindEvent(QuitGame);
        GetButton((int)Buttons.Explan).gameObject.BindEvent(ExplanGame);
        GetButton((int)Buttons.Developer).gameObject.BindEvent(ShowDeveloper);

        return true;
    }

    void ToGameScene()
    {
        // 게임 시작
        Debug.Log("게임 시작!");

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Managers.UI.ShowSceneUI<UI_Game>();
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


    }

    void ShowDeveloper()
    {
        // TODO
        // 개발자 소개..
        Debug.Log("하소연 프로젝트..!");


    }
}
