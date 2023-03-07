using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRepositioner : MonoBehaviour
{
    GameObject nearBlock;
    GameObject nearWall;
    float radius = 1.0f;
    //float radius = 0.8f;
    float Wradius = 1.5f;
    float bound;

    void Start()
    {
        nearBlock = Utils.FindNearestObject("Block", transform.position);
        nearWall = Utils.FindNearestObject("InstantiatedWall", transform.position);
        bound = 1.8f;

        if (nearBlock != null)
        {
            if (Vector3.Distance(transform.position, nearBlock.transform.position) < radius)
                transform.position += transform.position - nearBlock.transform.position;

            if (Mathf.Abs(transform.position.x) > bound)
            {
                Debug.Log("!!");
                float x = transform.position.x > 0 ? bound : (bound * (-1));
                transform.position = new Vector3(x, transform.position.y, 0);
            }
        }

        if (nearWall != null)
        {
            if (Vector3.Distance(transform.position, nearWall.transform.position) < Wradius)
                transform.position += transform.position - nearWall.transform.position;

            if (Mathf.Abs(transform.position.x) > bound)
            {
                float x = transform.position.x > 0 ? bound : (bound * (-1));
                transform.position = new Vector3(x, transform.position.y, 0);
            }
                
        }

    }

}
