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
        if(PlayerPrefs.HasKey("gold"))
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

        //if (Managers.Game.Mode != Define.Mode.StoryMode) return;

        //if (_player.transform.position.y < 100)
        //{
        //    // 도시 브금
        //}
        //else if (_player.transform.position.y < 500)
        //{
        //    // 에베레스트 브금
        //}
        //else if (_player.transform.position.y < 1000)
        //{
        //    // 하늘 세계 브금
        //}
        //else if (_player.transform.position.y < 2000)
        //{
        //    // 성층권 브금
        //}
        //else if (_player.transform.position.y < 5000)
        //{
        //    // 열권 브금
        //}
        //else
        //{
        //    // 우주 브금
        //}

        CutScene(); // 마우스 클릭 때마다 컷씬 변경
        
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
        // 마우스 클릭을 받기 위해 update로 넣음
        // 컷씬이 안 끝난 상태에서만, 스토리 모드에서만 동작하도록
        if (Managers.Cutscene.cutFinished == false && Managers.Game.Mode == Define.Mode.StoryMode)
        {
            // 마지막 컷씬이 끝나면 모든 컷씬 삭제
            if (Managers.Cutscene.SceneNumber == 3 && Input.GetMouseButtonDown(0))
                Managers.Cutscene.DistroyCutscene(); // 삭제
            // 클릭했을 때 다음 씬으로
            else if (Input.GetMouseButtonDown(0))
                Managers.Cutscene.PlayCutscene();
            // 게임 시작 시 컷씬 로드되도록; storymode 함수에 붙였을 땐 이상한 버그 발생해서 여기로 옮김
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
        // 컷씬 init
        Managers.Cutscene.InitCutsceneInfo();

        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_Lazy", Sound.Bgm);

        //GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/StoryModeBGImage");

        // 배경은 높이에 따라 달라지므로 높이를 측정하여 특정 높이가 됐을 때 브금 변경?
    }

    void ScoreMode()
    {
        // TODO 무한 배경 OR 우주니까 배경 고정?
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm); // TODO 스코어 모드 전용 브금으로 교체
        //GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/ScoreModeBGImage");
    }

    void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
