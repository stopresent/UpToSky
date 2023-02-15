using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public TrajectoryController trajectory;

    // -- player�� defaultPlayer�� ����
    // player�� ���� ���� �� 
    public PlayerController player;

    // �̴� ��
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
        // ī�޶� ������ ���� ī�޶�� �����Ѵ�.
        cam = Camera.main;
    }

    private void Update()
    {
        // cam.ScreenToWorldPoint(Input.mousePosition): ī�޶� z ���и�ŭ ������ �Ÿ��� ����� ���´�.
        // ����Ƽ�� �� ����� ��ũ���̶�� �����ϰ� �� ����� Ŭ���� �κ��� �޾ƿ´�.
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);

        // ��ġ���� ��,
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }

        // ��ġ�� ������ ��
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

    // �巡�� ������ �� ���� Ȱ��ȭ
    private void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    // �巡�� ���� �� �ǽð����� ������ �����Ѵ�.
    private void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = distance * direction * PushForce;

        trajectory.UpdateDots(player.pos, force);
    }

    // �巡�װ� ������ �� ������ ��Ȱ��ȭ ��Ű�� �÷��̾ �����δ�.
    private void OnDragEnd()
    {
        player.Push(force);
        trajectory.Hide();
    }
    #endregion

}
