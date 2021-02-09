using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayer : MonoBehaviour
{
    public GameObject[] gLayers;

    private void Awake()
    {
        foreach (var obj in gLayers)
        {
            obj.layer = 8;
            Debug.Log("Layer of " + obj.name + "is: " + obj.layer);
        }    
    }



}
