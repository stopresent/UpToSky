using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraBlock : MonoBehaviour
{
    SpriteRenderer SRenderer;

    private void Start()
    {
        SRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("decreaseA");
    }

    IEnumerator increaseA()
    {
        while(SRenderer.color.a < 1)
        {
            yield return new WaitForSeconds(0.0f);
            SRenderer.color += new Color(0, 0, 0, Time.fixedDeltaTime * 0.1f);
        }
        StartCoroutine("decreaseA");
    }

    IEnumerator decreaseA()
    {
        while (SRenderer.color.a > 0)
        {
            yield return new WaitForSeconds(0.0f);
            SRenderer.color -= new Color(0, 0, 0, Time.fixedDeltaTime * 0.1f);
        }
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine("increaseA");
    }
}
