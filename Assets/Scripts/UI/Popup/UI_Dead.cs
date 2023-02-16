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
    int gold;
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

        #region ����
        gold = GameObject.Find("UI_Game").GetComponent<UI_Game>().Gold;
        if (PlayerPrefs.HasKey("highestScore"))
            highestScore = PlayerPrefs.GetInt("highestScore") > highestScore ? PlayerPrefs.GetInt("highestScore") : highestScore;

        PlayerPrefs.SetInt("highestScore", highestScore);
        PlayerPrefs.SetInt("gold", gold);
        #endregion

        return true;
    }

    void ToScoreGameScene()
    {
        // ���� ����
        // ���丮 ��忡 �°� ���Ӿ��� �ٲ��� ��
        Debug.Log("���ھ� ��� ���� ����!");
        Managers.UI.ClosePopupUI(this);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Managers.UI.ShowSceneUI<UI_Game>();
    }

    void ToMainScene()
    {
        Debug.Log("���� �޴��� ���ƿ�");
        Managers.UI.ClosePopupUI(this);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        Managers.UI.ShowSceneUI<UI_Main>();
    }
}