using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject character;
    private bool done = false;
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
        if(other.gameObject.tag == "Player" && !done)
        {
            //Debug.Log("a");
            //transform.position = other.gameObject.transform.position;
            transform.rotation = other.gameObject.transform.rotation;
            done = true;
            float time = Random.Range(0.7f, 1.4f);
            Invoke("Revive", time);
        }
    }

    void Revive()
    {
        //Debug.Log("b");
        character.SetActive(true);
        done = false;
        character.transform.rotation = transform.rotation;
        gameObject.SetActive(false);
    }
}
