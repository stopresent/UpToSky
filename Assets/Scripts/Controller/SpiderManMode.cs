using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderManMode : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

}
