using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject pointerHead;

    public LayerMask raycastMask;

    private buttonScript selectedButton;
    private LineRenderer lineRenderer;
    private bool submit = true;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        SelectionInput();
        SubmitInput();
    }

    private void SelectionInput()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out hitInfo, Mathf.Infinity, raycastMask))
        {
            pointerHead.transform.position = hitInfo.point;
            lineRenderer.SetPosition(0, rightHand.transform.position);
            lineRenderer.SetPosition(1, hitInfo.point);

            if (true)
            {
                buttonScript uiButton;
                if (hitInfo.collider.gameObject.TryGetComponent(out uiButton))
                {
                    uiButton.SelectButton();
                    selectedButton = uiButton;
                }
                
            }
        }
        else
        {
            selectedButton = null;
            pointerHead.transform.position = rightHand.transform.position + rightHand.transform.forward * 1.2f;
            lineRenderer.SetPosition(0, rightHand.transform.position);
            lineRenderer.SetPosition(1, rightHand.transform.position + rightHand.transform.forward * 1.2f);
        }
    }

    private void SubmitInput()
    {
        if (Input.GetAxis("XRI_Right_Trigger") > 0.5f && submit)
        {
            submit = false;
            if (selectedButton)
            {
                selectedButton.PlayButton();
            }
        }
        if (Input.GetAxis("XRI_Right_Trigger") <= 0.5f)
            submit = true;
    }
}
