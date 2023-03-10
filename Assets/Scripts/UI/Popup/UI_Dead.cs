using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dead : UI_Popup
{
    enum images
    {
        RestartBtn,
        ReturnBtn,
    }

    enum Texts
    {
        ScoreText,
    }

    int highestScore;
    int gold;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;


        // TODO
        // Dead Effect


        BindImage(typeof(images));
        BindText(typeof(Texts));

        GetImage((int)images.RestartBtn).gameObject.BindEvent(ToScoreGameScene);
        GetImage((int)images.ReturnBtn).gameObject.BindEvent(ToMainScene);

        highestScore = GameObject.Find("UI_Game").GetComponent<UI_Game>().highestScore;
        GetText((int)Texts.ScoreText).text = $"Your Score : {highestScore}m";

        #region 저장
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

        // 게임 시작
        // 스토리 모드에 맞게 게임씬이 바뀌어야 함
        Managers.Sound.Clear();
        Managers.Sound.Play("Sound_CloseUI");
        Managers.UI.ClosePopupUI(this);

        #region
        // 전면 광고 추가
        Managers.Sound.Clear();

        Managers.Ads.ShowAd();
        //Managers.Ads.gameObject.GetComponent<ButtonBehaviour>().gameObject.GetComponent<Canvas>().sortingOrder = 20;

        // TODO
        // 꺼진거 확인

        #endregion

    }

    void ToMainScene()
    {
        Debug.Log("메인 메뉴로 돌아옴");
        Managers.Sound.Clear();
        Managers.Sound.Play("Sound_CloseUI");
        Managers.UI.ClosePopupUI(this);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        Managers.UI.ShowSceneUI<UI_Main>();
    }
}
