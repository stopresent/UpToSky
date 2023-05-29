using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }

    public enum Scene
    {
        Unknown,
        Dev,
        Game,
        Main,
        Ending,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        Max,
    }

    public enum Mode
    {
        Unknown,
        StoryMode,
        ScoreMode,
    }

    public enum Height
    {
        Mountain = 100,
        SkyWorld = 300,
        Stratosphere = 500,
        Thermosphere = 700,
        GalaxyBlues = 1004,
    }

    public enum State
    {
        None,
        BouncyState,
        Flying,
    }

}
