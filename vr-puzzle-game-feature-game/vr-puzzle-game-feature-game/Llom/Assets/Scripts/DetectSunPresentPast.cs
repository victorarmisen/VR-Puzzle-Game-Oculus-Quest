using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSunPresentPast : MonoBehaviour
{

    private BoxCollider collider;
    GameObject sun;
    public bool underSun = false;
    public bool past = false;
    private Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        sun = GameObject.FindWithTag("Sun");
        collider = GetComponent<BoxCollider>();


        Bounds bounds = mesh.bounds;
        Vector3 sunDir = sun.transform.forward;
        sunDir.Normalize();
        sunDir *= 100f;
        RaycastHit hit;
        if (!Physics.Raycast(transform.TransformPoint(0, bounds.size.y / 2f, 0), transform.TransformPoint(0, bounds.size.y / 2, 0) - sunDir, out hit, 100))
        {
            underSun = true;
            if (past) // inverts the way it works
                collider.enabled = false;
            else
                collider.enabled = true;
        }
        else
        {  
            underSun = false;
            if (past) // inverts the way it works
                collider.enabled = true;
            else
                collider.enabled = false;  
        }
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = mesh.bounds;
        Vector3 sunDir = sun.transform.forward;
        sunDir.Normalize();
        sunDir *= 100f;
        RaycastHit hit;

        //runs the logic to know if its shadowed or not
        if (!Physics.Raycast(transform.TransformPoint(0, bounds.size.y / 2f, 0), transform.TransformPoint(0, bounds.size.y / 2, 0) - sunDir, out hit, 100))
        {
            if (!underSun)
            {
               
                underSun = true;

                if (past) // inverts the way it works
                {
                    transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                    collider.enabled = false;
                }
                else
                {
                    
                    GetComponent<AudioSource>().Play();
                    collider.enabled = true;
                }
            }
        }
        else
        {
            if (underSun)
            {
                
                underSun = false;
                if (past) // inverts the way it works
                {
                    
                    GetComponent<AudioSource>().Play();
                    collider.enabled = true;
                }
                else
                {
                    collider.enabled = false;
                    transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                    
                    
                }
            }        
        }
    }
}
