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
        City = 100,
        Mountain = 150,
        SkyWorld = 250,
        Stratosphere = 400,
        Thermosphere = 650,
        GalaxyBlues = 1004,
    }

}
