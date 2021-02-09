using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionsTeleport : MonoBehaviour
{
    private Vector3 origin = Vector3.zero;
    private GameObject feedbackObj, feedbackLight;
    private Material materialBlue, materialRed;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && gameObject.layer == 6) //If teleport. 
        {
            transform.LookAt(origin, Vector3.up); //Simplemente de este if, utilizar esta linea, una vez hecho el teleport para
            //que el jugador mire al medio y no se desoriente. 
        }
        FeedbackCorrection(); // Cambio de color del feedback del teleport para saber si podemos hacer teleport a una superficie valida. 
    }

    void FeedbackCorrection()
    {
        if(gameObject.layer == 6)
        {
            feedbackObj.GetComponent<Renderer>().material = materialBlue;
            feedbackLight.GetComponent<Renderer>().material = materialBlue;
        } 
        else
        {
            feedbackObj.GetComponent<Renderer>().material = materialRed;
            feedbackLight.GetComponent<Renderer>().material = materialRed;
        }
    }

}
