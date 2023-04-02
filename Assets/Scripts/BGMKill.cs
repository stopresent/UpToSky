using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMKill : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("Bgm").GetComponent<AudioSource>().volume = 0.0f;
    }
}