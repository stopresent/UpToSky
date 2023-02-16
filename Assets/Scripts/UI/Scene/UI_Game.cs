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

        // ����� ���̿� ���� �޶����Ƿ� ���̸� �����Ͽ� Ư�� ���̰� ���� �� ��� ����?
    }

    void ScoreMode()
    {
        // TODO ���� ��� OR ���ִϱ� ��� ����?
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/Sound_GalaxyBlues", Sound.Bgm); // TODO ���ھ� ��� ���� ������� ��ü
        GameObject.Find("Test_BG").gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("Sprites/BG/ScoreModeBGImage");
    }

    void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
