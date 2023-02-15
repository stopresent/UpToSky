using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    int dotsNumber;
    [SerializeField] 
    GameObject dotsParent;
    [SerializeField]
    [Range(0.01f, 0.5f)] float dotMinScale;
    [SerializeField]
    [Range(0.5f, 1f)] float dotMaxScale;

    GameObject dotPrefab;
    float dotSpacing;

    Transform[] dotsList;
    Vector2 pos;
    float timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        init();
        Hide();
        PrepareDots();
    }

    private void init()
    {
        dotsNumber = 30;
        dotPrefab = Resources.Load<GameObject>("Prefabs/TrajectoryDot");
        dotSpacing = 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scalefactor = scale / dotsNumber;

        for(int i = 0; i <dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if(scale > dotMinScale)
            {
                scale -= scalefactor;
            }
        }
    }

    public void UpdateDots(Vector3 playerPos, Vector2 force)
    {
        timeStamp = dotSpacing;
        for(int i = 0; i < dotsNumber; i++)
        {
            pos.x = playerPos.x + force.x * timeStamp;
            pos.y = (playerPos.y + force.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }
    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
