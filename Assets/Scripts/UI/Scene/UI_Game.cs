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

    void StoryMode()
    {

        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_Lazy", Sound.Bgm);
        GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/StoryModeBGImage");
        //GetImage((int)Images.StoryBG).gameObject.SetActive(true);

        // ����� ���̿� ���� �޶����Ƿ� ���̸� �����Ͽ� Ư�� ���̰� ���� �� ��� ����?
    }

    void ScoreMode()
    {
        // TODO ���� ��� OR ���ִϱ� ��� ����?
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
