using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    Vector3 auxPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.transform.position - (transform.up * 1.1f);
        if (Vector3.Dot(camera.transform.up, transform.up) > 0.05f)
            auxPos = ((Vector3.ProjectOnPlane(camera.transform.forward, transform.up).normalized) * 0.2f);
        transform.position -= auxPos;
        
    }
}
