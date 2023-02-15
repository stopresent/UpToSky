using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public TrajectoryController trajectory;

    // -- player와 defaultPlayer의 차이
    // player가 여러 개일 수 
    public PlayerController player;

    // 미는 힘
    float PushForce = 4f;
    Camera cam;

    bool isDragging;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    private void Start()
    {
        // 카메라 변수를 메인 카메라로 설정한다.
        cam = Camera.main;
    }

    private void Update()
    {
        // cam.ScreenToWorldPoint(Input.mousePosition): 카메라에 z 성분만큼 떨어진 거리에 평면을 놓는다.
        // 유니티는 그 평면이 스크린이라고 생각하고 그 평면의 클릭한 부분을 받아온다.
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);

        // 터치했을 때,
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }

        // 터치가 끝났을 때
        if(Input.GetMouseButtonUp(0))
        {
            if(player != null)
            {
                isDragging = false;
                OnDragEnd();
            }
        }
        if (isDragging)
        {
            OnDrag();
        }
    }

    #region Drag

    // 드래그 시작할 때 궤적 활성화
    private void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    // 드래그 중일 때 실시간으로 궤적을 변경한다.
    private void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = distance * direction * PushForce;

        trajectory.UpdateDots(player.pos, force);
    }

    // 드래그가 끝났을 때 궤적을 비활성화 시키고 플레이어를 움직인다.
    private void OnDragEnd()
    {
        player.Push(force);
        trajectory.Hide();
    }
    #endregion

}
