using UnityEngine;

//This include will be only in a level_manager script in a future
using UnityEngine.SceneManagement;

public class RotateSun : MonoBehaviour
{
    [SerializeField] bool manualRotation = false;
    [SerializeField] float speed = 0.1f;

    Vector3 offset;

    void Start()
    {
        offset = gameObject.transform.rotation.eulerAngles;
    }

    void Update()
    {
        //***This code will be in a level_Manager script in a future
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Load Main manu scene
            //Level_Loader.getInstance().Loadlevel(0);
        }
        //***

        if (manualRotation)
        {
            offset.y += Input.GetAxis("Horizontal") * speed;
        }
        else
        {
            offset.y += speed;
            if (offset.y > 360f)
                offset.y = 0;
        }

        gameObject.transform.rotation = Quaternion.Euler(offset);
    }
}
