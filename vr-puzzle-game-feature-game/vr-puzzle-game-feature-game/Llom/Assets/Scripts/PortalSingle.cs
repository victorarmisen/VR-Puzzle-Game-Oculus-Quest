using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSingle : MonoBehaviour
{
    public GameObject targetPortal;
    public bool isObjective;
    public bool isHit;
    private GameObject camera;
    private Vector3 cameraPos;
    private LayerMask mask;
    private bool onCooldown = false;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        mask = LayerMask.GetMask("Portals");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isObjective)
        {
            cameraPos = camera.transform.position;
            Vector3 dir1 = cameraPos - transform.position;
            Vector3 dir2 = cameraPos - targetPortal.transform.position;
            Debug.Log(name + Vector3.Dot(dir2.normalized, dir1.normalized));
            active = true;
            if (Vector3.Dot(dir2.normalized, dir1.normalized) > 0.9995f)
            {
                Debug.DrawRay(transform.position, dir1, Color.red);
                active = true;
                targetPortal.GetComponent<PortalSingle>().Activate(true);
            }
            else
            {
                Debug.DrawRay(transform.position, dir1, Color.green);
                active = false;
                targetPortal.GetComponent<PortalSingle>().Activate(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (active && !onCooldown)
            {
                PortalCooldown();
                targetPortal.GetComponent<PortalSingle>().PortalCooldown();

                Vector3 targetPos = targetPortal.transform.position;
                targetPos.y = (targetPos.y - 1f) + 1.35f;
                other.gameObject.transform.position = targetPos;
                other.gameObject.transform.rotation = transform.rotation;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    public void PortalCooldown()
    {
        onCooldown = true;
        Invoke("ResetPortal", 1.5f);
    }

    private void ResetPortal()
    {
        onCooldown = false;
    }

    public void Activate(bool activate)
    {
        active = activate;
    }
}
