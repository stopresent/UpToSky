using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D pRigid;

    private void Start()
    {
        pRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BreakableBlock")
            Destroy(collision.gameObject, 3f);
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            pRigid.velocity += Vector2.up * 7;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pRigid.velocity += Vector2.left * 2;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pRigid.velocity += Vector2.right * 2;
        }

    }
}
