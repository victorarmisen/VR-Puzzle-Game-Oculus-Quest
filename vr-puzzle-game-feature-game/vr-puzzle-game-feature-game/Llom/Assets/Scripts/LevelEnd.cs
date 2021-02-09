using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    public int numberOfCharacters;
    int counter = 0;
    public int nextLevel;
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
        if (other.transform.tag == "Player")
        {
            other.gameObject.SetActive(false);
            counter++;
            //Debug.Log("Reached End");
            if (counter >= numberOfCharacters)
                SceneManager.LoadScene(nextLevel); ;
        }
    }
}
