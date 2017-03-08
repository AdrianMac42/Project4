using UnityEngine;
using System.Collections;

public class PlayerMoveWASD : MonoBehaviour {

    float xAxisValue;
    float yAxisValue;
    WorldStats world;

    // Use this for initialization
    void Start () {
        world = (GameObject.Find("GameController")).GetComponent<WorldStats>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, world.worldWidth - 1), Mathf.Clamp(transform.position.y, 0, world.worldHeight - 1), transform.position.z);

        xAxisValue = Input.GetAxisRaw("Horizontal");
        yAxisValue = Input.GetAxisRaw("Vertical");
        if (gameObject != null)
        {
            gameObject.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));
        }
        gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, world.worldWidth - 1 ), Mathf.Clamp(transform.position.y, 0, world.worldHeight -1), transform.position.z);


    }
}
