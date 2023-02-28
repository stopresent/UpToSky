using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRepositioner : MonoBehaviour
{
    GameObject nearBlock;
    GameObject nearWall;
    float radius = 1.2f;
    float Wradius = 2.6f;
    float bound;

    void Start()
    {
        nearBlock = Utils.FindNearestObject("Block", transform.position);
        nearWall = Utils.FindNearestObject("InstantiatedWall", transform.position);
        bound = GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>().max.x;

        if (nearBlock != null)
        {
            if (Vector3.Distance(transform.position, nearBlock.transform.position) < radius)
                transform.position += transform.position - nearBlock.transform.position;

            if (Mathf.Abs(transform.position.x) > bound)
                transform.position = new Vector3(transform.position.x > 0 ? bound : -bound, transform.position.y, 0);
        }

        if (nearWall != null)
        {
            if (Vector3.Distance(transform.position, nearWall.transform.position) < Wradius)
                transform.position += transform.position - nearWall.transform.position;

            if (Mathf.Abs(transform.position.x) > bound)
                transform.position = new Vector3(transform.position.x > 0 ? bound : -bound, transform.position.y, 0);
        }

    }

}
