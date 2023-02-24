using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelectMode : UI_Popup
{
    enum Buttons
    {
        StoryModeBtn,
        ScoreModeBtn,
    }

    enum Images
    {
        Block,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        GetButton((int)Buttons.StoryModeBtn).gameObject.BindEvent(ToStoryGameScene);
        GetButton((int)Buttons.ScoreModeBtn).gameObject.BindEvent(ToScoreGameScene);

        GetImage((int)Images.Block).gameObject.BindEvent(Close);

        return true;
    }

    void ToStoryGameScene()
    {
        // ���� ����
        // ���丮 ��忡 �°� ���Ӿ��� �ٲ��� ��
        Debug.Log("���丮 ��� ���� ����!");
        Managers.UI.ClosePopupUI(this);

        Time.timeScale = 1;
        Managers.Game.Mode = Define.Mode.StoryMode;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Managers.UI.ShowSceneUI<UI_Game>();
    }

    void ToScoreGameScene()
    {
        // ���� ����
        // ���丮 ��忡 �°� ���Ӿ��� �ٲ��� ��
        Debug.Log("���ھ� ��� ���� ����!");
        Managers.UI.ClosePopupUI(this);

        Time.timeScale = 1;
        Managers.Game.Mode = Define.Mode.ScoreMode;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Managers.UI.ShowSceneUI<UI_Game>();
    }

    void Close()
    {
        Managers.Sound.Play("Sound_CloseUI");
        Managers.UI.ClosePopupUI(this);
    }

}
