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
        // ���� ����
        Debug.Log("���� ����!");

        Managers.UI.ShowPopupUI<UI_SelectMode>();

        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        //Managers.UI.ShowSceneUI<UI_Game>();
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

    }

    void Collection()
    {
        Debug.Log("�ݷ��� â");
        Managers.UI.ShowPopupUI<UI_Collection>();
    }

    void ShowDeveloper()
    {
        // TODO
        // ������ �Ұ�..
        Debug.Log("�ϼҿ� ������Ʈ..!");
        Managers.UI.ShowPopupUI<UI_ShowDeveloper>();

    }
}
