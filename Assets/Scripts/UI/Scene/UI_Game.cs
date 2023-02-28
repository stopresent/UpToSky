using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static Define;

public class UI_Game : UI_Scene
{
    //public GameObject _player;

    enum Buttons
    {
        SettingBtn,
        //SpiderManModeBtn,
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
    public int PrevBlockSpawnH;
    public int BlockSpawnHInterval = 2; // �� 2���͸��� ����� �����ȴ�
    public int Gold;
    public int PrevIncomeH = 0;
    public int GoldIncomeHInterval = 10; // �� 10���͸��� ��带 �޴´�
    public float PlayTime = 0.0f;

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // ��Ų ���� Ȯ��

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.SettingBtn).gameObject.BindEvent(Setting);
        //GetButton((int)Buttons.SpiderManModeBtn).gameObject.BindEvent(SpiderManMode);
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
        CutScene(); // ���콺 Ŭ�� ������ �ƾ� ����
        
        PlayTime += Time.deltaTime;
        Score = (int)GameObject.Find("Player").transform.position.y;
        if (highestScore < Score)
            highestScore = Score;

        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            if (Score < (int)Define.Height.City)
            {
                // ���ú��
                if (Managers.Sound.GetCurrent().clip == null || Managers.Sound.GetCurrent().clip.name != "Sound_City")
                    Managers.Sound.Play("BGM/Sound_City", Sound.Bgm);
            }
            else if (Score < (int)Define.Height.Mountain)
            {
                // ��������Ʈ ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Mountain")
                    Managers.Sound.Play("BGM/Sound_Mountain", Sound.Bgm);
            }
            else if (Score < (int)Define.Height.SkyWorld)
            {
                // �ϴ� ���� ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_SkyWorld")
                    Managers.Sound.Play("BGM/Sound_SkyWorld", Sound.Bgm);
            }
            else if (Score < (int)Define.Height.Stratosphere)
            {
                // ������ ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Stratosphere")
                    Managers.Sound.Play("BGM/Sound_Stratosphere", Sound.Bgm);
            }
            else if (Score < (int)Define.Height.Thermosphere)
            {
                // ���� ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Thermosphere")
                    Managers.Sound.Play("BGM/Sound_Thermosphere", Sound.Bgm);
            }
            else if(Score <= (int)Define.Height.GalaxyBlues)
            {
                // ���� ���
                if (Managers.Sound.GetCurrent().clip.name != "Sound_GalaxyBlues")
                {
                    // ���ַ� ���� �߷� ������
                    GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0.4f;

                    Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm);
                }
            }
            else
            {
                // �ȵ�θ޴� ����
                Managers.Sound.Clear();

                UnityEngine.SceneManagement.SceneManager.LoadScene("EndingScene");
                Managers.UI.ShowSceneUI<UI_Ending>();
            }
        }

        if (Score > PrevBlockSpawnH + BlockSpawnHInterval)
        {
            PrevBlockSpawnH = Score;
            GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>().Spawn = true;
        }
        RefreshUI();
        GoldIncomeByHeight();
    }

    void RefreshUI()
    {
        GetText((int)Texts.ScoreText).text = String.Format("{0:#,###}", $"{Score}");
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

        if (PlayerPrefs.HasKey("Soundness"))
            Managers.Sound.GetCurrent().volume = PlayerPrefs.GetFloat("Soundness");
        if (PlayerPrefs.GetInt("IsMute") == 1)
        {
            PlayerPrefs.SetFloat("Soundness", Managers.Sound.GetCurrent().volume);
            Managers.Sound.GetCurrent().volume = 0.0f;
        }

        Managers.Resource.Instantiate("StoryModeBG");

        //GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/StoryModeBGImage");
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

        GameObject scoreModeBG = Managers.Resource.Instantiate("ScoreModeBG");
        GameObject elavator = GameObject.Find("Elevator");
        if (elavator != null)
            scoreModeBG.transform.SetParent(elavator.transform);
    }

    void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
    
    void SpiderManMode()
    {
        GameObject.Find("Player").AddComponent<SpiderManMode>();
    }
}
