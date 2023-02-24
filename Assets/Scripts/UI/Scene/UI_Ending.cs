using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEngine.SocialPlatforms.Impl;

public class UI_Ending : UI_Scene
{
    enum Images
    {
        EndingImage,
        SettingImage,
    }

    enum Buttons
    {
        ToMainBtn,
    }

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Sound.Clear();

        BindImage(typeof(Images));
        BindButton(typeof(Buttons));

        GetImage((int)Images.EndingImage).gameObject.BindEvent(CloseEndingImage);
        GetButton((int)Buttons.ToMainBtn).gameObject.BindEvent(ToMain);

        return true;
    }

    void CloseEndingImage()
    {
        GetImage((int)Images.EndingImage).gameObject.SetActive(false);

        // 클리어 축하 사운드
        Managers.Sound.Play("BGM/Sound_Clear", Sound.Bgm);
    }

    void ToMain()
    {
        Managers.Sound.Clear();

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        Managers.UI.ShowSceneUI<UI_Main>();
    }

}
