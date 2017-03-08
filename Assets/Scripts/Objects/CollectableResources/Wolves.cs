using UnityEngine;
using System.Collections;

public class Wolves : MonoBehaviour {

    public int food = 50;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update () {
	    if (food <= 0)
        {
            Destroy(gameObject);
        }
	}
}
