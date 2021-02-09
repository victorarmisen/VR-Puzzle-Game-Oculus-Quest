using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private bool active = true;
    [SerializeField] bool outRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"/* && active*/)
        {
            if(outRotation)
            {
                other.GetComponent<Movement>().changeOutPlane = true;
                other.GetComponent<Movement>().changePlane = false;
            }
            else
            {
                other.GetComponent<Movement>().changePlane = true;
                other.GetComponent<Movement>().changeOutPlane = false;
            }
            /*active = false;
            Invoke("Activate", 2f);*/
        }
    }

    private void Activate()
    {
        active = true;
    }
}
