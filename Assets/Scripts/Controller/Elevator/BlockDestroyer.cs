using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" || 
            collision.gameObject.tag == "Coin" ||   
            collision.gameObject.tag == "InstantiatedWall")
            Destroy(collision.gameObject);

    }


}