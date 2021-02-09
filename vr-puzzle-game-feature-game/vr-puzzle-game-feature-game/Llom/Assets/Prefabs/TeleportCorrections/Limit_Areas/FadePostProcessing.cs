using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FadePostProcessing : MonoBehaviour
{

    public PostProcessVolume volume;
    private Vignette viggnette;
    public bool mode = false;

    void Start()
    {
        volume.profile.TryGetSettings(out viggnette);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            mode = false;
        }
        Fade(1.2f);
    }

    void Fade(float value)
    {
        if(!mode)
        {
            viggnette.intensity.value += value * Time.deltaTime;
            if (viggnette.intensity.value >= 1.0f)
                mode = true;
        } 
        else
        {
            viggnette.intensity.value -= value * Time.deltaTime; 
        }
        viggnette.intensity.value = Mathf.Clamp(viggnette.intensity.value, 0.0f, 100.0f);
    }

}
