using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour
{
    public TrajectoryController trajectory;
 
    public PlayerController player;

    // �̴� ��
    float PushForce = 6f;
    Camera cam;

    bool isDragging;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    public Vector2 force;
    float distance;

    private void Start()
    {
        // ī�޶� ������ ���� ī�޶�� �����Ѵ�.
        cam = Camera.main;
    }

    private void Update()
    {
        if (player == null)
            return;

        if (!player.isContactAnything)
            return;

        if (PlayerPrefs.GetInt("OnSettingUI") == 1)
            return;

        // cam.ScreenToWorldPoint(Input.mousePosition): ī�޶� z ���и�ŭ ������ �Ÿ��� ����� ���´�.
        // ����Ƽ�� �� ����� ��ũ���̶�� �����ϰ� �� ����� Ŭ���� �κ��� �޾ƿ´�.
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);

        // ��ġ���� ��, UI�� ��ġ���� ���� ���� �� �ϰ�(�ƾ� �� ���� ����Ǵ� �� ��������)
        if (!EventSystem.current.IsPointerOverGameObject()&&Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            Managers.Sound.Play("Sound_Charging");
            OnDragStart();
        }

        // ��ġ�� ������ ��
        if(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
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
        force.x = distance * direction.x * PushForce;
        force.y = Mathf.Min(distance * direction.y * PushForce, 30);

        trajectory.UpdateDots(player.pos, force);
    }

    // �巡�װ� ������ �� ������ ��Ȱ��ȭ ��Ű�� �÷��̾ �����δ�.
    private void OnDragEnd()
    {
        if(player.GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezeAll)
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.Push(force);
        trajectory.Hide();
    }
    #endregion

}
