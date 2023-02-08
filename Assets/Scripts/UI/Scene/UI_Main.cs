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
        // ���� ����
        Debug.Log("���� ����!");

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Managers.UI.ShowSceneUI<UI_Game>();
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


    }

    void ShowDeveloper()
    {
        // TODO
        // ������ �Ұ�..
        Debug.Log("�ϼҿ� ������Ʈ..!");


    }
}
