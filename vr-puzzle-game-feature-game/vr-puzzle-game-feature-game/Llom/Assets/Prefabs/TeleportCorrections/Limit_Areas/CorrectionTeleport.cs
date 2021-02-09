using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectionTeleport : MonoBehaviour
{
    private GameObject touchingObject;
    private GameObject teleportDetection;

    void CorrectionSameWall()
    {
        RaycastHit hitInfo = new RaycastHit();
        if(teleportDetection == touchingObject && hitInfo.normal == new Vector3(0,1,0))
        {
            //DO TELEPORT.
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == 6)
        {
            touchingObject = col.gameObject;
        }
    }
}
