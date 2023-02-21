using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Collection : UI_Popup
{
    enum Images
    {
        Block,
    }

    enum Buttons
    {
        CharacterSkinTabBtn,
        EndingCollectionTabBtn,
    }

    enum GameObjects
    {
        CharacterSkinTab,
        EndingCollectionTab,
    }

    public enum PlayTab
    {
        CharacterSkin,
        EndingCollection,
    }

    PlayTab _tab = PlayTab.CharacterSkin;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetImage((int)Images.Block).gameObject.BindEvent(Close);

        GetButton((int)Buttons.CharacterSkinTabBtn).gameObject.BindEvent(() => ShowTab(PlayTab.CharacterSkin));
        GetButton((int)Buttons.EndingCollectionTabBtn).gameObject.BindEvent(() => ShowTab(PlayTab.EndingCollection));
        GetObject((int)GameObjects.CharacterSkinTab).gameObject.SetActive(true);
        GetObject((int)GameObjects.EndingCollectionTab).gameObject.SetActive(false);

        return true;
    }

    public void ShowTab(PlayTab tab)
    {
        _tab = tab;

        GetObject((int)GameObjects.CharacterSkinTab).gameObject.SetActive(false);
        GetObject((int)GameObjects.EndingCollectionTab).gameObject.SetActive(false);

        switch (_tab)
        {
            case PlayTab.CharacterSkin:
                GetObject((int)GameObjects.CharacterSkinTab).gameObject.SetActive(true);
                break;
            case PlayTab.EndingCollection:
                GetObject((int)GameObjects.EndingCollectionTab).gameObject.SetActive(true);
                break;
        }

    }

    void Close()
    {
        Managers.Sound.Play("Sound_CloseUI");
        Managers.UI.ClosePopupUI(this);
    }
}
