using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    float xAxisValue;
    float yAxisValue;
    Camera thisCamera;
    WorldStats world;

    // Use this for initialization
    void Start () {
        thisCamera = gameObject.GetComponent<Camera>();
        world = GameObject.Find("GameController").GetComponent<WorldStats>();

    }
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 3, world.worldWidth - 3), Mathf.Clamp(transform.position.y, 3, world.worldHeight - 3), transform.position.z);

        gameObject.transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, gameObject.transform.position.z);
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
            if (thisCamera.orthographicSize >= 15)
            {
                return;
            }
            else
            {
                //transform.position.x = 500;
                thisCamera.orthographicSize += 1;
            }
        }
        gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 3, world.worldWidth - 3), Mathf.Clamp(transform.position.y, 3, world.worldHeight - 3), transform.position.z);



    }
}
