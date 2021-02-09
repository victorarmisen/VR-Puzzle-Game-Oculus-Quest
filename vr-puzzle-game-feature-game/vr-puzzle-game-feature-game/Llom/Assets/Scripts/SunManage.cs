using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManage : MonoBehaviour
{
    public GameObject leftController;
    public Vector3 sp, pp;
    public Material tpMat, defMat;
    private bool move = false;
    private LineRenderer lineRenderer;
    public GameObject Cylinder;

    void MoveSun(Vector3 dir)
    {
        dir = new Vector3(dir.x, Mathf.Clamp(dir.y, 0.7f,0.8f), dir.z);
        transform.position = dir * 1000;
        transform.LookAt(-dir);
    }
    void MoveSun()
    {
        //Cylinder.SetActive(false);
        Vector3 dir = new Vector3(leftController.transform.forward.x, Mathf.Clamp(leftController.transform.forward.y, 0.6f, 0.8f), leftController.transform.forward.z);
        transform.position = dir * 1000;
        transform.LookAt(-dir);
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("XRI_Left_Trigger") > 0.5f)
        {
            Cylinder.SetActive(true);
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, leftController.transform.position);
            lineRenderer.SetPosition(1, leftController.transform.position + leftController.transform.forward * 1.2f);

            leftController.GetComponent<MeshRenderer>().material = tpMat;
            move = true;
        }
        else 
        {
            Cylinder.SetActive(false);
            lineRenderer.enabled = false;
            leftController.GetComponent<MeshRenderer>().material = defMat;
            move = false;
        }

        if(move)
            MoveSun();

    }

    
}
