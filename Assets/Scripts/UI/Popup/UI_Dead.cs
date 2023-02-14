using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dead : UI_Popup
{
    enum Buttons
    {
        RestartBtn,
        ReturnBtn,
    }

    enum Texts
    {
        RestartText,
        ReturnText,
        ScoreText,
    }

    int highestScore;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.RestartBtn).gameObject.BindEvent(ToScoreGameScene);
        GetButton((int)Buttons.ReturnBtn).gameObject.BindEvent(ToMainScene);

        highestScore = GameObject.Find("UI_Game").GetComponent<UI_Game>().highestScore;
        GetText((int)Texts.ScoreText).text = $"Your Score : {highestScore}m";

        // 하이스코어 저장
        if (PlayerPrefs.HasKey("highestScore"))
            highestScore = PlayerPrefs.GetInt("highestScore") > highestScore ? PlayerPrefs.GetInt("highestScore") : highestScore;

        PlayerPrefs.SetInt("highestScore", highestScore);

        return true;
    }

    void ToScoreGameScene()
    {
        // 게임 시작
        // 스토리 모드에 맞게 게임씬이 바뀌어야 함
        Debug.Log("스코어 모드 게임 시작!");
        Managers.UI.ClosePopupUI(this);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Managers.UI.ShowSceneUI<UI_Game>();
    }

    void ToMainScene()
    {
        Debug.Log("메인 메뉴로 돌아옴");
        Managers.UI.ClosePopupUI(this);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        Managers.UI.ShowSceneUI<UI_Main>();
    }
}
