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
    public int GoldIncomeHInterval = 10; // �� 10���͸��� ��带 �޴´�

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

        #region ��� �ҷ�����
        if (PlayerPrefs.HasKey("gold"))
            Gold = PlayerPrefs.GetInt("gold");
        #endregion

        // TODO ��濡 ���� ����� �ٲ��� ��
        // TODO ��忡 ���� ���Ӿ� �ٲ�
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
                // ���ú��
                if (Managers.Sound.GetCurrent().clip == null || Managers.Sound.GetCurrent().clip.name != "Sound_City")
                    Managers.Sound.Play("BGM/Sound_City", Sound.Bgm);
                Debug.Log($"{Managers.Sound.GetCurrent().name}");
            }
            else if (Score < 500)
            {
                // ��������Ʈ ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Mountain")
                    Managers.Sound.Play("BGM/Sound_Mountain", Sound.Bgm);
            }
            else if (Score < 1000)
            {
                // �ϴ� ���� ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_SkyWorld")
                    Managers.Sound.Play("BGM/Sound_SkyWorld", Sound.Bgm);
            }
            else if (Score < 2000)
            {
                // ������ ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Stratosphere")
                    Managers.Sound.Play("BGM/Sound_Stratosphere", Sound.Bgm);
            }
            else if (Score < 5000)
            {
                // ���� ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Thermosphere")
                    Managers.Sound.Play("BGM/Sound_Thermosphere", Sound.Bgm);
            }
            else
            {
                // ���� ���
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

        // ����� ���̿� ���� �޶����Ƿ� ���̸� �����Ͽ� Ư�� ���̰� ���� �� ��� ����?
    }

    void ScoreMode()
    {
        // TODO ���� ��� OR ���ִϱ� ��� ����?
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm); // TODO ���ھ� ��� ���� ������� ��ü
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
