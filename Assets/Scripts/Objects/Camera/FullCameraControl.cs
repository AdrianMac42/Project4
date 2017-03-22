using UnityEngine;
using System.Collections;

public class FullCameraControl : MonoBehaviour {

    float xAxisValue;
    float yAxisValue;
    Camera thisCamera;
    GenWorld world;

    // Use this for initialization
    void Start()
    {
        thisCamera = gameObject.GetComponent<Camera>();
        world = GameObject.Find("GameController").GetComponent<GenWorld>();

    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -world.x + 3, world.x - 3), Mathf.Clamp(transform.position.y, -world.y + 3, world.y - 3), transform.position.z);

        xAxisValue = Input.GetAxisRaw("Horizontal");
        yAxisValue = Input.GetAxisRaw("Vertical");
        if (gameObject != null)
        {
            gameObject.transform.Translate(new Vector3(xAxisValue* (thisCamera.orthographicSize / 20), yAxisValue* (thisCamera.orthographicSize / 20), 0.0f));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (thisCamera.orthographicSize <= 5)
            {
                return;
            }
            else
            { 

                //transform.position.x = 500;
                thisCamera.orthographicSize -= 1;
            }
        }

        // Mouse wheel moving backwards
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (thisCamera.orthographicSize >= 30)
            {
                return;
            }
            else
            {
                //transform.position.x = 500;
                thisCamera.orthographicSize += 1;
            }
        }

        gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -world.x + 3, world.x - 3), Mathf.Clamp(transform.position.y, -world.y + 3, world.y - 3), transform.position.z);



    }
}
