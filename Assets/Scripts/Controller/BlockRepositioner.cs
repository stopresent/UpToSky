using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRepositioner : MonoBehaviour
{
    GameObject nearBlock;

    void Start()
    {
        nearBlock = Utils.FindNearestObject("Block", transform.position);

        if (nearBlock == null)
            return;

        if( Vector3.Distance(transform.position, nearBlock.transform.position) < 1.2f )
        {
            Debug.Log(nearBlock.transform.position.ToString());
            Debug.Log("자리 재지정" + transform.position.ToString() + " 거리 : " + Vector3.Distance(transform.position, nearBlock.transform.position));

            transform.position += transform.position - nearBlock.transform.position;
        }

        if (Mathf.Abs(transform.position.x) > 2)
            transform.position = new Vector3(2, transform.position.y, 0);

    }

}
