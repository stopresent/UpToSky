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
            playerAlive = false;

            #region
            // 전면 광고 추가
            Managers.Sound.Clear();

            Managers.Ads.LoadInterstitialAd();
            Managers.Ads.ShowAd();
            //Managers.Ads.gameObject.GetComponent<ButtonBehaviour>().gameObject.GetComponent<Canvas>().sortingOrder = 20;

            // TODO
            // 꺼진거 확인

            #endregion
        }
    }
}