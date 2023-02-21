using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_Setting : UI_Popup
{
    AudioSource CurrentBGM = Managers.Sound.GetCurrent();

    bool _isMute = false;
    bool _isOpen = false;

    enum Buttons
    {
        BackBtn,
        MuteBtn,
    }

    enum Objects
    {
        SoundSlider,
    }

    private void Update()
    {
        if (_isMute == true) return;

        SoundControl();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Sound.Play("Sound_OpenUI");

        Time.timeScale = 0;

        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        GetButton((int)Buttons.BackBtn).gameObject.BindEvent(BackToGame);
        GetButton((int)Buttons.MuteBtn).gameObject.BindEvent(MuteSound);

        GetObject((int)Objects.SoundSlider).gameObject.BindEvent(SoundControl);

        ShowMuteIcon();

        if (PlayerPrefs.HasKey("Soundness"))
            GetObject((int)Objects.SoundSlider).gameObject.GetOrAddComponent<Slider>().value = PlayerPrefs.GetFloat("Soundness");

        _isOpen = true;
        PlayerPrefs.SetInt("OnSettingUI", System.Convert.ToInt16(_isOpen));

        return true;
    }

    void ShowMuteIcon()
    {
        if (PlayerPrefs.GetInt("IsMute") == 0)
        {
            GetButton((int)Buttons.MuteBtn).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Setting/MuteOff");
            _isMute = false;
        }
        else
        {
            GetButton((int)Buttons.MuteBtn).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Setting/MuteOn");
            _isMute = true;
        }
    }

    void BackToGame()
    {
        if (!_isMute)
            PlayerPrefs.SetFloat("Soundness", CurrentBGM.volume);
        PlayerPrefs.SetInt("IsMute", System.Convert.ToInt16(_isMute));

        _isOpen= false;
        PlayerPrefs.SetInt("OnSettingUI", System.Convert.ToInt16(_isOpen));

        Time.timeScale = 1;
        Managers.UI.ClosePopupUI(this);
        Managers.Sound.Play("Sound_CloseUI");

    }

    void SoundControl()
    {
        if (_isMute) return;
        CurrentBGM.volume = GetObject((int)Objects.SoundSlider).gameObject.GetOrAddComponent<Slider>().value;
    }

    void MuteSound()
    {
        if (_isMute == false)
        {
            PlayerPrefs.SetFloat("Soundness", CurrentBGM.volume);
            Managers.Sound.GetCurrent().volume = 0.0f;
            GetButton((int)Buttons.MuteBtn).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Setting/MuteOn");
            _isMute = true;
        }
        else
        {
            Managers.Sound.GetCurrent().volume = PlayerPrefs.GetFloat("Soundness");
            GetButton((int)Buttons.MuteBtn).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Setting/MuteOff");
            _isMute = false;
        }

    }
}
