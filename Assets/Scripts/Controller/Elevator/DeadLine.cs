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
            // ���� ���� �߰�
            Managers.Sound.Clear();

            Managers.Ads.LoadInterstitialAd();
            Managers.Ads.ShowAd();
            //Managers.Ads.gameObject.GetComponent<ButtonBehaviour>().gameObject.GetComponent<Canvas>().sortingOrder = 20;

            // TODO
            // ������ Ȯ��

            #endregion
        }
    }
}