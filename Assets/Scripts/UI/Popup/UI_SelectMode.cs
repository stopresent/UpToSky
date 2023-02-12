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
        // 게임 시작
        Debug.Log("스토리 모드 게임 시작!");
        Managers.UI.ClosePopupUI(this);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Managers.UI.ShowSceneUI<UI_Game>();
    }

    void ToScoreGameScene()
    {
        // 게임 시작
        Debug.Log("스코어 모드 게임 시작!");
        Managers.UI.ClosePopupUI(this);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Managers.UI.ShowSceneUI<UI_Game>();
    }

    void Close()
    {
        Managers.UI.ClosePopupUI(this);
    }

}
