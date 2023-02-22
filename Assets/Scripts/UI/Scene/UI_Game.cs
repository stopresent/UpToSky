using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if(PlayerPrefs.HasKey("gold"))
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

        //if (Managers.Game.Mode != Define.Mode.StoryMode) return;

        //if (_player.transform.position.y < 100)
        //{
        //    // ���� ���
        //}
        //else if (_player.transform.position.y < 500)
        //{
        //    // ��������Ʈ ���
        //}
        //else if (_player.transform.position.y < 1000)
        //{
        //    // �ϴ� ���� ���
        //}
        //else if (_player.transform.position.y < 2000)
        //{
        //    // ������ ���
        //}
        //else if (_player.transform.position.y < 5000)
        //{
        //    // ���� ���
        //}
        //else
        //{
        //    // ���� ���
        //}

        CutScene(); // ���콺 Ŭ�� ������ �ƾ� ����
        
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

    void CutScene()
    {
        // ���콺 Ŭ���� �ޱ� ���� update�� ����
        // �ƾ��� �� ���� ���¿�����, ���丮 ��忡���� �����ϵ���
        if (Managers.Cutscene.cutFinished == false && Managers.Game.Mode == Define.Mode.StoryMode)
        {
            // ������ �ƾ��� ������ ��� �ƾ� ����
            if (Managers.Cutscene.SceneNumber == 3 && Input.GetMouseButtonDown(0))
                Managers.Cutscene.DistroyCutscene(); // ����
            // Ŭ������ �� ���� ������
            else if (Input.GetMouseButtonDown(0))
                Managers.Cutscene.PlayCutscene();
            // ���� ���� �� �ƾ� �ε�ǵ���; storymode �Լ��� �ٿ��� �� �̻��� ���� �߻��ؼ� ����� �ű�
            else if (Managers.Cutscene.SceneNumber == 0)
                Managers.Cutscene.PlayCutscene();
        }
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
        // �ƾ� init
        Managers.Cutscene.InitCutsceneInfo();

        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_Lazy", Sound.Bgm);

        //GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/StoryModeBGImage");

        // ����� ���̿� ���� �޶����Ƿ� ���̸� �����Ͽ� Ư�� ���̰� ���� �� ��� ����?
    }

    void ScoreMode()
    {
        // TODO ���� ��� OR ���ִϱ� ��� ����?
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm); // TODO ���ھ� ��� ���� ������� ��ü
        //GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/ScoreModeBGImage");
    }

    void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
