using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static Define;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UI_Game : UI_Scene
{
    //public GameObject _player;

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
    public int GoldIncomeHInterval = 10; // 매 10미터마다 골드를 받는다

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        //_player = GameObject.Find("Player").gameObject;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.SettingBtn).gameObject.BindEvent(Setting);

        #region 골드 불러오기
        if (PlayerPrefs.HasKey("gold"))
            Gold = PlayerPrefs.GetInt("gold");
        #endregion

        // TODO 배경에 따라 브금이 바뀌어야 함
        // TODO 모드에 따라 게임씬 바뀜
        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            StoryMode();
        }
        else if (Managers.Game.Mode == Define.Mode.ScoreMode)
        {
            ScoreMode();
        }

        return true;
    }

    private void Update()
    {
        Score = (int)GameObject.Find("Player").transform.position.y;
        if (highestScore < Score)
            highestScore = Score;

        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            if (Score < 100)
            {
                // 도시브금
                if (Managers.Sound.GetCurrent().clip == null || Managers.Sound.GetCurrent().clip.name != "Sound_City")
                    Managers.Sound.Play("BGM/Sound_City", Sound.Bgm);
                Debug.Log($"{Managers.Sound.GetCurrent().name}");
            }
            else if (Score < 500)
            {
                // 에베레스트 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Mountain")
                    Managers.Sound.Play("BGM/Sound_Mountain", Sound.Bgm);
            }
            else if (Score < 1000)
            {
                // 하늘 세계 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_SkyWorld")
                    Managers.Sound.Play("BGM/Sound_SkyWorld", Sound.Bgm);
            }
            else if (Score < 2000)
            {
                // 성층권 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Stratosphere")
                    Managers.Sound.Play("BGM/Sound_Stratosphere", Sound.Bgm);
            }
            else if (Score < 5000)
            {
                // 열권 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Thermosphere")
                    Managers.Sound.Play("BGM/Sound_Thermosphere", Sound.Bgm);
            }
            else
            {
                // 우주 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_GalaxyBlues")
                    Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm);
            }
        }

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
        if (Score > PrevIncomeH + GoldIncomeHInterval)
        {
            PrevIncomeH = Score;
            Gold += 1;
        }
    }

    void StoryMode()
    {
        Managers.Sound.Clear();

        if (PlayerPrefs.HasKey("Soundness"))
            Managers.Sound.GetCurrent().volume = PlayerPrefs.GetFloat("Soundness");
        if (PlayerPrefs.GetInt("IsMute") == 1)
        {
            PlayerPrefs.SetFloat("Soundness", Managers.Sound.GetCurrent().volume);
            Managers.Sound.GetCurrent().volume = 0.0f;
        }

        Managers.Resource.Instantiate("StoryModeBG");

        // 배경은 높이에 따라 달라지므로 높이를 측정하여 특정 높이가 됐을 때 브금 변경?
    }

    void ScoreMode()
    {
        // TODO 무한 배경 OR 우주니까 배경 고정?
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm); // TODO 스코어 모드 전용 브금으로 교체
        if (PlayerPrefs.HasKey("Soundness"))
            Managers.Sound.GetCurrent().volume = PlayerPrefs.GetFloat("Soundness");
        if (PlayerPrefs.GetInt("IsMute") == 1)
        {
            PlayerPrefs.SetFloat("Soundness", Managers.Sound.GetCurrent().volume);
            Managers.Sound.GetCurrent().volume = 0.0f;
        }

        Managers.Resource.Instantiate("ScoreModeBG");
    }

    void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
