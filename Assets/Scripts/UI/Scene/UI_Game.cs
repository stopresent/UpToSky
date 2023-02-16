using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    enum Images
    {
        ScoreImage,
    }

    enum GameObjects
    {

    }


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Managers.Game.Mode != Define.Mode.StoryMode) return;

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

    void StoryMode()
    {

        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_Lazy", Sound.Bgm);
        GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/StoryModeBGImage");

        // 배경은 높이에 따라 달라지므로 높이를 측정하여 특정 높이가 됐을 때 브금 변경?
    }

    void ScoreMode()
    {
        // TODO 무한 배경 OR 우주니까 배경 고정?
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm); // TODO 스코어 모드 전용 브금으로 교체
        GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/ScoreModeBGImage");
    }

    void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
