using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        BG,
    }

    enum GameObjects
    {

    }

    public int highestScore;
    public int Score;
    public int PrevBlockSpawnH;
    public int BlockSpawnHInterval = 3; // 매 2미터마다 블록이 스폰된다
    public int Gold;
    public int PrevIncomeH = 0;
    public int GoldIncomeHInterval = 10; // 매 10미터마다 골드를 받는다
    public float PlayTime = 0.0f;

    Animator anim;

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // 스킨 착용 확인
        if (GameObject.Find("Player") != null)
            anim = GameObject.Find("Player").GetComponent<Animator>();

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.SettingBtn).gameObject.BindEvent(Setting);
        GetImage((int)Images.BG).gameObject.BindEvent(GetMouseDown, UIEvent.PointerDown);
        GetImage((int)Images.BG).gameObject.BindEvent(GetMouseDrag, UIEvent.Pressed);
        GetImage((int)Images.BG).gameObject.BindEvent(GetMouseUp, UIEvent.PointerUp);
        //GetButton((int)Buttons.SpiderManModeBtn).gameObject.BindEvent(SpiderManMode);
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

        #region Anim
        // 애니메이션

        if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y > 0.1) anim.Play("JumpUp");
        else if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y < -0.1) anim.Play("JumpDown");
        else anim.Play("IDLE");

        if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x < -0.1) GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX = true;
        else if (GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x > 0.1) GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX = false;

        #endregion

        PlayTime += Time.deltaTime;
        Score = (int)GameObject.Find("Player").transform.position.y * 3;


        if (highestScore < Score)
            highestScore = Score;

        SetBgm();

        if (Score > PrevBlockSpawnH + BlockSpawnHInterval)
        {
            PrevBlockSpawnH = Score;
            GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>().Spawn = true;
        }
        RefreshUI();
        GoldIncomeByHeight();
    }

    public void SetBgm()
    {
        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            if (Score < (int)Define.Height.Mountain)
            {
                if (Managers.Sound.GetCurrent().clip != null && Managers.Sound.GetCurrent().clip.name == "Sound_SkyWorld") return;

                // 에베레스트 브금
                if (Managers.Sound.GetCurrent().clip == null || Managers.Sound.GetCurrent().clip.name != "Sound_Mountain")
                    Managers.Sound.Play("BGM/Sound_Mountain", Sound.Bgm);
            }
            else if (Score < (int)Define.Height.SkyWorld)
            {
                if (Managers.Sound.GetCurrent().clip.name == "Sound_Stratosphere") return;

                // 하늘 세계 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_SkyWorld")
                    Managers.Sound.Play("BGM/Sound_SkyWorld", Sound.Bgm);
            }
            else if (Score < (int)Define.Height.Stratosphere)
            {
                if (Managers.Sound.GetCurrent().clip.name == "Sound_Thermosphere") return;

                // 성층권 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Stratosphere")
                    Managers.Sound.Play("BGM/Sound_Stratosphere", Sound.Bgm);
            }
            else if (Score < (int)Define.Height.Thermosphere)
            {
                if (Managers.Sound.GetCurrent().clip.name == "Sound_GalaxyBlues") return;

                // 열권 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_Thermosphere")
                    Managers.Sound.Play("BGM/Sound_Thermosphere", Sound.Bgm);
            }
            else if (Score <= (int)Define.Height.GalaxyBlues)
            {

                // 우주 브금
                if (Managers.Sound.GetCurrent().clip.name != "Sound_GalaxyBlues")
                {
                    // 우주로 가면 중력 낮아짐
                    GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0.4f;

                    Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm);
                }
            }
            else
            {
                // 안드로메다 도착
                Managers.Sound.Clear();

                UnityEngine.SceneManagement.SceneManager.LoadScene("EndingScene");
                Managers.UI.ShowSceneUI<UI_Ending>();
            }
        }
    }

    public void GetMouseDown()
    {
        if (Managers.Game.State != Define.State.None) return;

        anim.gameObject.GetComponent<PlayerController2>().OnMouseDown();
    }

    public void GetMouseDrag()
    {
        if (Managers.Game.State != Define.State.None) return;

        anim.gameObject.GetComponent<PlayerController2>().OnMouseDrag();
    }

    public void GetMouseUp()
    {
        if (Managers.Game.State != Define.State.None) return;

        anim.gameObject.GetComponent<PlayerController2>().OnMouseUp();
    }

    void RefreshUI()
    {
        GetText((int)Texts.ScoreText).text = String.Format("{0:#,###}", $"{Score}");
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

        for (int i = 1; i < 11; i++)
        {
            GameObject Bg = Managers.Resource.Instantiate($"Bg{i}");

            float BgY = Bg.GetComponent<SpriteRenderer>().bounds.max.y;
            Bg.transform.position = new Vector3(0, (2 * (i - 1) * BgY + BgY), 10);
        }


    }

    void ScoreMode()
    {
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm); // 스코어 모드 전용 브금으로 교체
        if (PlayerPrefs.HasKey("Soundness"))
            Managers.Sound.GetCurrent().volume = PlayerPrefs.GetFloat("Soundness");
        if (PlayerPrefs.GetInt("IsMute") == 1)
        {
            PlayerPrefs.SetFloat("Soundness", Managers.Sound.GetCurrent().volume);
            Managers.Sound.GetCurrent().volume = 0.0f;
        }

        // 무한 배경 OR 우주니까 배경 고정
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
