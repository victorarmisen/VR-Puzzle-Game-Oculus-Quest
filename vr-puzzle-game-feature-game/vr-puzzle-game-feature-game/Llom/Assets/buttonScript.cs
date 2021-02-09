using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{
    public Button buttonRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectButton()
    {
        if (buttonRef != null)
            buttonRef.Select();
        
    }

    public void PlayButton()
    {
        if (buttonRef != null)
            buttonRef.onClick.Invoke();
    }

    public void DeselectButton()
    {
        //if (buttonRef != null)
    }
}
