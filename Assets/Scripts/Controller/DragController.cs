using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public TrajectoryController trajectory;
    public PlayerController[] player;
    private PlayerController defultPlayer;
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
        cam = Camera.main;
    }

    private void Update()
    {
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            defultPlayer = null;
            for (int i = 0; i < player.Length; i++)
            {
                if (player[i].col == Physics2D.OverlapPoint(pos))
                {
                    Time.timeScale = 0.1f;
                    isDragging = true;
                    defultPlayer = player[i];
                    OnDragStart();
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(defultPlayer != null)
            {
                Time.timeScale = 1f;
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
    private void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    private void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = distance * direction * PushForce;

        trajectory.UpdateDots(defultPlayer.pos, force);
    }

    private void OnDragEnd()
    {
        defultPlayer.Push(force);
        trajectory.Hide();
    }
    #endregion

}
