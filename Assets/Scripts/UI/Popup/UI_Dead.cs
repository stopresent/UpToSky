using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Managers.Sound.Clear();
        Managers.Sound.Play("Sound_CloseUI");
        Managers.UI.ClosePopupUI(this);
        
        Managers.Game.AdCount++;

        if (Managers.Game.AdCount % 3 != 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            return;
        }

        #region

        // ���� ���� �߰�
        Managers.Sound.Clear();

        Managers.Ads.ShowAd();

        #endregion

    }

    void ToMainScene()
    {
        Debug.Log("���� �޴��� ���ƿ�");
        Managers.Sound.Clear();
        Managers.Sound.Play("Sound_CloseUI");
        Managers.UI.ClosePopupUI(this);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        Managers.UI.ShowSceneUI<UI_Main>();
    }
}
