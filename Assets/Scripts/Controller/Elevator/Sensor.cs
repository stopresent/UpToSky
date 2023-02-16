using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public float stayingTime = 0.0f;
    GameObject Elevator;

    private void Start()
    {
        Elevator = transform.parent.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        stayingTime += Time.deltaTime;
        Elevator.GetComponent<Rigidbody2D>().velocity = collision.attachedRigidbody.velocity * 0.8f;

        if (collision.GetComponent<Rigidbody2D>().velocity.y == 0)
            Elevator.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(Vector2.up * 6, Vector2.up * 1, 0.5f);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Elevator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}