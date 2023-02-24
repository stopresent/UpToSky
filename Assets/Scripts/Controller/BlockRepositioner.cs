using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRepositioner : MonoBehaviour
{
    GameObject nearBlock;
    float radius = 1.2f;
    void Start()
    {
        nearBlock = Utils.FindNearestObject("Block", transform.position);

        if (nearBlock == null)
            return;

        if( Vector3.Distance(transform.position, nearBlock.transform.position) < radius)
            transform.position += transform.position - nearBlock.transform.position;

        if (Mathf.Abs(transform.position.x) > 2)
            transform.position = new Vector3(2, transform.position.y, 0);

    }

}
