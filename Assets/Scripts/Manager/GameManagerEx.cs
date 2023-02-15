using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    Define.Mode _mode = Define.Mode.Unknown;

    public Define.Mode Mode { get { return _mode; } set { _mode = value; } }

}
