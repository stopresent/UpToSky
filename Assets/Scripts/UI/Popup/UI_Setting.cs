using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Setting : UI_Popup
{
    enum Buttons
    {
        BackBtn,
    }

    enum Texts
    {

    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.BackBtn).gameObject.BindEvent(Back);

        return true;
    }

    void Back()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
