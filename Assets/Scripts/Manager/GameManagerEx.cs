using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using System;


public class GameManagerEx
{
    Define.Mode _mode = Define.Mode.Unknown;

    public Define.Mode Mode { get { return _mode; } set { _mode = value; } }

    int _maxBlockCount = 100;
    int _currentBlockCount = 0;

    public int MaxBlockCount { get { return _maxBlockCount; } }
    public int CurrentBlockCount { get { return _currentBlockCount; } set { _currentBlockCount = value; } }


}
