using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    // ������ ����� ���� ����
    int dotsNumber;

    // ������ ���� ������ �ϳ��� ���δ� �θ� ������Ʈ
    [SerializeField] 
    GameObject dotsParent;

    // ������ ���� ���� �ּ� ũ��� �ִ� ũ��
    [SerializeField]
    [Range(0.01f, 0.5f)] float dotMinScale;
    [SerializeField]
    [Range(0.5f, 1f)] float dotMaxScale;

    // ������ ���� ���� ������
    GameObject dotPrefab;

    // ���� ����
    float dotSpacing;

    // ������ ��ġ, ũ�⸦ ��ȯ�� Transform �迭
    Transform[] dotsList;

    // ������ ��ġ�� ��Ÿ�� ����
    Vector2 pos;

    // ������ ��Ÿ�� �� ���� ������� �ð�
    float timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        init();
        Hide();
        PrepareDots();
    }

    // �ʱ�ȭ
    private void init()
    {
        // ������ ������ 30, ������ 0.03f, �������� ��θ� �����Ѵ�. 
        dotsNumber = 30;
        dotPrefab = Resources.Load<GameObject>("Prefabs/TrajectoryDot");
        dotSpacing = 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ������ ���� �� �����յ��� �غ��� ���´�.
    void PrepareDots()
    {
        // ������ ������ŭ Transform �迭�� �����.
        dotsList = new Transform[dotsNumber];

        // �������� ũ�⸦ �ִ� ũ��� ����
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        // �������� ũ�⸦ ������ ������ => ���߿� �������� ũ�⿡�� �� ���� ���� ������� ũ�� ������ �� ���δ�.
        float scale = dotMaxScale;
        float scalefactor = scale / dotsNumber;

        // ���� ������ŭ
        for(int i = 0; i <dotsNumber; i++)
        {
            // �ν��Ͻ�ȭ�� �������� transform ������ �Ʊ� ���� transform �迭�� �����Ѵ�.
            // -- �ν��Ͻ�ȭ��?: �̹� ������� ���� ������Ʈ�� �ʿ��� ������ �ǽð����� �����.
            dotsList[i] = Instantiate(dotPrefab, null).transform;

            // �ν��Ͻ��� �������� �θ�� �Ʊ� ������ ������ �θ�� ���Ѵ�.
            dotsList[i].parent = dotsParent.transform;

            // ���� ũ��� ó������ �ִ� ũ������
            dotsList[i].localScale = Vector3.one * scale;

            // ������ ���� �����ν� ���� ���� ũ���� ���� �����Ѵ�.
            if(scale > dotMinScale)
            {
                scale -= scalefactor;
            }
        }
    }

    // ���� ��ġ ��ǥ ���
    public void UpdateDots(Vector3 playerPos, Vector2 force)
    {
        // �ϴ� �� ������ �ð����� �����Ѵ�.
        timeStamp = dotSpacing;
        for(int i = 0; i < dotsNumber; i++)
        {
            // pos.x: ���� �÷��̾��� x ��ǥ ��ġ�� timeStamp ���� force�� �̿��� ����Ѵ�.
            // pos.y: ���� �÷��̾��� y ��ǥ ��ġ�� timeStamp ���� force�� �̿��� ����ϰ�, �߷��� ��ġ�� ���� ������ ��ģ��.
            pos.x = playerPos.x + force.x * timeStamp;
            pos.y = (playerPos.y + force.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            // �迭�� ��� ���� ��ġ ���� �����Ѵ�.
            dotsList[i].position = pos;

            // timestamp ���� ���ݸ�ŭ ���� ���� ���� �� ��ǥ�� ���Ѵ�.
            timeStamp += dotSpacing;
        }
    }

    // ���� Ȱ��ȭ
    public void Show()
    {
        dotsParent.SetActive(true);
    }

    // ���� ��Ȱ��ȭ
    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
