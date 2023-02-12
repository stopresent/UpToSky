using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        return true;
    }

    void ToGameScene()
    {
        // 게임 시작
        Debug.Log("게임 시작!");

        Managers.UI.ShowPopupUI<UI_SelectMode>();

        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        //Managers.UI.ShowSceneUI<UI_Game>();
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

    }

    void Collection()
    {
        Debug.Log("콜렉션 창");
        Managers.UI.ShowPopupUI<UI_Collection>();
    }

    void ShowDeveloper()
    {
        // TODO
        // 개발자 소개..
        Debug.Log("하소연 프로젝트..!");
        Managers.UI.ShowPopupUI<UI_ShowDeveloper>();

    }
}
