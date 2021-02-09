using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator animator;
    public Canvas canvas;
    void Start()
    {
        canvas = transform.GetChild(0).GetComponent<Canvas>();
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void FadeTeleport()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeOut()
    {
    }
}
