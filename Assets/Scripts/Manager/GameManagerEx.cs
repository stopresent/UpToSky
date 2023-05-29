using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using System;
using Unity.VisualScripting;

public class GameManagerEx
{
    Define.Mode _mode = Define.Mode.Unknown;
    public Define.Mode Mode { get { return _mode; } set { _mode = value; } }

    Define.State _state = Define.State.None;
    public Define.State State { get { return _state; } set { _state = value; } }

    public int AdCount = 0;
}

