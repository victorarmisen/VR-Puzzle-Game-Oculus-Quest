using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerTeleport : MonoBehaviour
{
    public Camera cam; // Center eye camera 
    public GameObject TeleportFeedback, TeleportFeedbackLight;
    public GameObject rightController;
    public Material tpMat, defMat, feedbackON, feedbackOFF;
    private Vector3 origin = Vector3.zero;
    private GameObject actualTouchingObject;
    //public Animator animator;
    private bool tp = true;
    private LineRenderer lineRenderer;

    //Fade Post proccesing 
    public PostProcessVolume volume;
    private Vignette viggnette;
    private bool FADEMODE = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        actualTouchingObject = null;
        volume.profile.TryGetSettings(out viggnette);
    }

    void Update()
    {
        RaycastHit hitPos;
        if (Physics.Raycast(rightController.transform.position, rightController.transform.forward,
            out hitPos, Mathf.Infinity))
        {
            TeleportFeedback.SetActive(true);
            TeleportFeedback.transform.position = hitPos.point;
            TeleportFeedback.transform.rotation = Quaternion.FromToRotation(TeleportFeedback.transform.up,hitPos.normal) * TeleportFeedback.transform.rotation;
            lineRenderer.SetPosition(0, rightController.transform.position);
            lineRenderer.SetPosition(1, hitPos.point);

        }
        else
        {
            TeleportFeedback.SetActive(false);
            lineRenderer.SetPosition(0, rightController.transform.position);
            lineRenderer.SetPosition(1, rightController.transform.position + rightController.transform.forward * 1.2f);
        }

        CheckTeleportCollision();

    }

    void CheckTeleportCollision()
    {

        if (Input.GetAxis("XRI_Right_Trigger") > 0.5f && tp)
        {
            tp = false;
            
            RaycastHit hitPos;
            if (Physics.Raycast(rightController.transform.position, rightController.transform.forward, out hitPos, Mathf.Infinity) )
            {
                RaycastHit hitPos1;
                Physics.Raycast(transform.position, -transform.up, out hitPos1, Mathf.Infinity);
                if ((hitPos1.collider.gameObject != hitPos.collider.gameObject) || 
                    (hitPos1.collider.gameObject == hitPos.collider.gameObject && hitPos.normal == hitPos1.normal))
                {

                    rightController.GetComponent<MeshRenderer>().material = tpMat;

                    transform.position = hitPos.point + (hitPos.normal * 1.5f);
                    transform.rotation = Quaternion.FromToRotation(transform.up, hitPos.normal) * transform.rotation;

                    //Fade & Sound
                    FADEMODE = false;
                    GetComponent<AudioSource>().Play();
                }

            }
        }
        Fade(1.2f); //Fade 
        if (Input.GetAxis("XRI_Right_Trigger") <= 0.5f)
        {
            tp = true;
            
            rightController.GetComponent<MeshRenderer>().material = defMat;
        }

    }

    void FeedbackCorrection(GameObject feedbackTeleport, GameObject feedbackLight, GameObject ToTeleport)
    {
        if (ToTeleport.layer == 8)
        {
            feedbackTeleport.GetComponent<Renderer>().material = feedbackON;
            feedbackLight.GetComponent<Renderer>().material = feedbackON;
        }
        else
        {
            feedbackTeleport.GetComponent<Renderer>().material = feedbackOFF;
            feedbackLight.GetComponent<Renderer>().material = feedbackOFF;
        }
    }

    void Fade(float value)
    {
        if (!FADEMODE)
        {
            viggnette.intensity.value += value * Time.deltaTime;
            if (viggnette.intensity.value >= 1.0f)
                FADEMODE = true;
        }
        else
        {
            viggnette.intensity.value -= value * Time.deltaTime;
        }
        viggnette.intensity.value = Mathf.Clamp(viggnette.intensity.value, 0.0f, 100.0f);
    }

}

