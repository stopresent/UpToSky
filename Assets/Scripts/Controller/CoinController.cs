using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        GameObject.Find("UI_Game").GetComponent<UI_Game>().Gold += 1;
        Managers.Sound.Play("Sound_GetCoin");
        Destroy(gameObject);
    }

}
