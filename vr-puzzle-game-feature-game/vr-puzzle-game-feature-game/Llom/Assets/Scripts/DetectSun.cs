using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSun : MonoBehaviour
{
    GameObject sun;
    private bool underSun = false;
    private Mesh mesh;
    private Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        movement = GetComponent<Movement>();

        sun = GameObject.FindWithTag("Sun");

    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = mesh.bounds;
        Vector3 sunDir = sun.transform.forward;
        sunDir.Normalize();
        sunDir *= 100f;
        RaycastHit hit;

        /*if (!Physics.Raycast(transform.TransformPoint(0, bounds.size.y /2f, 0), transform.TransformPoint(0, bounds.size.y / 2, 0) - sunDir, out hit, 30))
        {
            Debug.DrawLine(transform.TransformPoint(0, bounds.size.y / 2, 0), transform.TransformPoint(0, bounds.size.y / 2, 0) - sunDir, Color.red);
            if (!movement.inSun)
            {
                movement.alive = true;
                movement.inSun = true;
                //movement.PauseActor();
                movement.ActivateActor();
            }
        }
        else
        {
            Debug.DrawLine(transform.TransformPoint(0, bounds.size.y / 2, 0), transform.TransformPoint(0, bounds.size.y / 2, 0) - sunDir, Color.green);
            if (movement.inSun)
            {
                movement.alive = false;
                movement.inSun = false;
                //movement.ActivateActor();
                movement.PauseActor();
            }

        }*/
    }
}
