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

    public int AdCount = 0;
}

