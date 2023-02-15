using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_Game : UI_Scene
{
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
        //StoryBG,
        //ScoreBG,
    }

    enum GameObjects
    {

    }


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
        BindImage(typeof(Images));

        GetButton((int)Buttons.SettingBtn).gameObject.BindEvent(Setting);
        //GetImage((int)Images.StoryBG).gameObject.SetActive(false);
        //GetImage((int)Images.ScoreBG).gameObject.SetActive(false);

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
        //GetImage((int)Images.StoryBG).gameObject.SetActive(true);

        // 배경은 높이에 따라 달라지므로 높이를 측정하여 특정 높이가 됐을 때 브금 변경?
    }

    void ScoreMode()
    {
        // TODO 무한 배경 OR 우주니까 배경 고정?
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_Lazy", Sound.Bgm);
        GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/ScoreModeBGImage");
        //GetImage((int)Images.ScoreBG).gameObject.SetActive(true);
    }

    void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
