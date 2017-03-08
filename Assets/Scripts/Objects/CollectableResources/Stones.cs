using UnityEngine;
using System.Collections;

public class Stones : MonoBehaviour {

    public int stone = 150;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update () {
	    if (stone <= 0)
        {
            Destroy(gameObject);
        }
	}
}
