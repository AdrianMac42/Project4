using UnityEngine;
using System.Collections;

public class Trees : MonoBehaviour {

    public int wood = 150;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update () {
	    if (wood <= 0)
        {
            Destroy(gameObject);
        }
	}
}
