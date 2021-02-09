using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{
    [SerializeField] Transform camera;
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private Vector3 dirA;
    private Vector3 dirB;
    private Vector3 dirC;
    private Vector3 cameraPos;
    private Mesh mesh;
    private BoxCollider collider;
    private bool collideA = false;
    private bool collideB = false;
    private bool collideC = false;
    int layerMask = 1 << 0;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = camera.position;
        Bounds bounds = mesh.bounds;
        pointA = transform.TransformPoint(bounds.size.x/2, 0, 0);
        pointB = transform.TransformPoint(-bounds.size.x / 2, 0,0);
        pointC = transform.TransformPoint(0, 0, 0);
        dirA = cameraPos - pointA;
        dirB = cameraPos - pointB;
        dirC = cameraPos - pointC;
        Debug.DrawRay(pointA, dirA, Color.green);
        Debug.DrawRay(pointB, dirB, Color.green);
        Debug.DrawRay(pointC, dirC, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(pointA, dirA, out hit, dirA.magnitude, layerMask))
        {
            Debug.DrawRay(pointA, dirA, Color.red);
            collideA = true;
        }
        else
        {
            collideA = false;
        }
        if (Physics.Raycast(pointB, dirB, out hit, dirB.magnitude))
        {
            Debug.DrawRay(pointB, dirB, Color.red);
            collideB = true;
        }
        else
        {
            collideB = false;
        }
        if (Physics.Raycast(pointC, dirC, out hit, dirC.magnitude))
        {
            Debug.DrawRay(pointC, dirC, Color.red);            
            collideC = true;
        }
        else
        {
            collideC = false;
        }

        if(collideA && collideB && collideC)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }
}
