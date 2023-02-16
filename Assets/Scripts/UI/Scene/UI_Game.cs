using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UI_Game : UI_Scene
{
    enum Buttons
    {
        SettingBtn,
    }

    enum Texts
    {
        ScoreText,
        GoldText,
    }

    enum Images
    {
        ScoreImage,
    }

    enum GameObjects
    {

    }

    public int highestScore;
    public int Score;
    public int Gold;
    public int PrevIncomeH = 0;
    public int GoldIncomeHInterval = 10; // �� 10���͸��� ��带 �޴´�

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

        GetButton((int)Buttons.SettingBtn).gameObject.BindEvent(Setting);

        #region ��� �ҷ�����
        if(PlayerPrefs.HasKey("gold"))
            Gold = PlayerPrefs.GetInt("gold");
        #endregion

        return true;
    }

    private void Update()
    {
        Score = (int)GameObject.Find("Player").transform.position.y;
        if(highestScore < Score)
            highestScore = Score;

        RefreshUI();
        GoldIncomeByHeight();
    }

    void RefreshUI()
    {
        GetText((int)Texts.ScoreText).text = String.Format("{0:#,###} m", $"{Score}");
        GetText((int)Texts.GoldText).text = String.Format("{0:#,##0}", Gold);
    }

    void GoldIncomeByHeight()
    {
        if ( Score > PrevIncomeH + GoldIncomeHInterval )
        {
            PrevIncomeH = Score;
            Gold += 1;
        }
    }

    void Setting()
    {
        // TODO
        // setting â ���� �ð� ����
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
