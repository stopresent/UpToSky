using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HowToGame : UI_Popup
{
    enum Images
    {
        Block,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));

        GetImage((int)Images.Block).gameObject.BindEvent(Close);

        return true;
    }

    void Close()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
