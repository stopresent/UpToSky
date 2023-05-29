using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathController : MonoBehaviour
{
    static GameObject clone = null;

    public static void StartVisualizingPath(GameObject player)
    {
        //player.GetComponent<Rigidbody2D>().simulated = false;
        //player.GetComponent<CircleCollider2D>().enabled= false;

        clone = Instantiate(player, player.transform.position, Quaternion.identity);
        clone.tag = "Clone";
        clone.layer = 10;
        clone.transform.localScale = Vector3.one * 0.25f;
        player.GetComponent<PlayerController2>().State = Define.State.None;
        clone.GetComponent<Rigidbody2D>().simulated = true;
        clone.GetComponent<CircleCollider2D>().enabled = true;
        var color = new Color(255/255f, 255/255f, 255/255f);
        color.a = 100/255f;
        clone.GetComponent<SpriteRenderer>().color = color;
        clone.GetComponent<TrailRenderer>().enabled = false;
        Physics2D.simulationMode = SimulationMode2D.Script;
    }

    public static void VisualizePath(GameObject player, Vector3 force)
    {
        clone.transform.position = player.transform.position;

        Rigidbody2D cloneRigidBody = clone.GetComponent<Rigidbody2D>();
        cloneRigidBody.velocity = Vector3.zero;
        cloneRigidBody.AddForce(force);

        List<Vector3> pathPoints = new List<Vector3>();
        int simulationSteps = 1000;
        for(int i = 1; i <simulationSteps; i++)
        {
            Physics2D.Simulate(Time.fixedDeltaTime);
            pathPoints.Add(cloneRigidBody.transform.position);
        }

        LineRenderer linePath = cloneRigidBody.GetComponent<LineRenderer>();
        linePath.enabled = true;
        linePath.positionCount = pathPoints.Count;
        linePath.SetPositions(pathPoints.ToArray());
    }

    public static void StopVisualizingPath(GameObject player)
    {
        player.GetComponent<CircleCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().simulated = true;
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        Destroy(clone);
    }
}
