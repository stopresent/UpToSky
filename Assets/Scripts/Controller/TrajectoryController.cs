using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    // 궤적에 사용할 원의 개수
    int dotsNumber;

    // 궤적에 사용된 원들을 하나로 감싸는 부모 오브젝트
    [SerializeField] 
    GameObject dotsParent;

    // 궤적에 사용된 원의 최소 크기와 최대 크기
    [SerializeField]
    [Range(0.01f, 0.5f)] float dotMinScale;
    [SerializeField]
    [Range(0.5f, 1f)] float dotMaxScale;

    // 궤적에 사용된 원의 프리팹
    GameObject dotPrefab;

    // 궤적 간격
    float dotSpacing;

    // 원들의 위치, 크기를 반환할 Transform 배열
    Transform[] dotsList;

    // 원들의 위치를 나타낼 벡터
    Vector2 pos;

    // 궤적을 나타낼 때 쓰일 상대적인 시간
    float timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        init();
        Hide();
        PrepareDots();
    }

    // 초기화
    private void init()
    {
        // 원들의 개수를 30, 간격을 0.03f, 프리팹의 경로를 지정한다. 
        dotsNumber = 30;
        dotPrefab = Resources.Load<GameObject>("Prefabs/TrajectoryDot");
        dotSpacing = 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 앞으로 쓰일 원 프리팹들을 준비해 놓는다.
    void PrepareDots()
    {
        // 원들의 개수만큼 Transform 배열을 만든다.
        dotsList = new Transform[dotsNumber];

        // 프리팹의 크기를 최대 크기로 지정
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        // 프리팹의 크기를 개수로 나눈다 => 나중에 프리팹의 크기에서 한 번씩 빼는 방식으로 크기 조절할 때 쓰인다.
        float scale = dotMaxScale;
        float scalefactor = scale / dotsNumber;

        // 원의 개수만큼
        for(int i = 0; i <dotsNumber; i++)
        {
            // 인스턴스화한 프리팹의 transform 정보를 아까 만든 transform 배열에 저장한다.
            // -- 인스턴스화란?: 이미 만들어진 게임 오브젝트를 필요할 때마다 실시간으로 만든다.
            dotsList[i] = Instantiate(dotPrefab, null).transform;

            // 인스턴스한 프리팹의 부모는 아까 위에서 지정한 부모로 정한다.
            dotsList[i].parent = dotsParent.transform;

            // 원의 크기는 처음에는 최대 크기지만
            dotsList[i].localScale = Vector3.one * scale;

            // 적절한 값을 뺌으로써 점점 작은 크기의 원을 생성한다.
            if(scale > dotMinScale)
            {
                scale -= scalefactor;
            }
        }
    }

    // 궤적 위치 좌표 계산
    public void UpdateDots(Vector3 playerPos, Vector2 force)
    {
        // 일단 원 간격을 시간으로 지정한다.
        timeStamp = dotSpacing;
        for(int i = 0; i < dotsNumber; i++)
        {
            // pos.x: 다음 플레이어의 x 좌표 위치를 timeStamp 값과 force를 이용해 계산한다.
            // pos.y: 다음 플레이어의 y 좌표 위치를 timeStamp 값과 force를 이용해 계산하고, 중력이 미치는 물리 계산까지 마친다.
            pos.x = playerPos.x + force.x * timeStamp;
            pos.y = (playerPos.y + force.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            // 배열에 방금 구한 위치 값을 저장한다.
            dotsList[i].position = pos;

            // timestamp 값을 간격만큼 더해 다음 궤적 원 좌표를 구한다.
            timeStamp += dotSpacing;
        }
    }

    // 궤적 활성화
    public void Show()
    {
        dotsParent.SetActive(true);
    }

    // 궤적 비활성화
    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
