using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteOutController : MonoBehaviour
{
    void Start()
    {

    }

    void FixedUpdate()
    {
        Whiting();
    }

    void Whiting()
    {
        if (GetComponent<RectTransform>().localScale.x >= 1000)
            return;
        GetComponent<RectTransform>().localScale *= 1.02f;
    }
}
