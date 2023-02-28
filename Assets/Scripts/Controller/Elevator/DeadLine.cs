using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    bool playerAlive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player" && playerAlive == true)
        {
            Destroy(GameObject.Find("Player").GetComponent<PlayerController>());
            Managers.UI.ShowPopupUI<UI_Dead>();
            playerAlive = false;


        }
    }
}