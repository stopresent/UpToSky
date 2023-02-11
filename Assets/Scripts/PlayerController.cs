using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mousePos;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);

    }


}
