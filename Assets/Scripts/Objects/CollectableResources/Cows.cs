using UnityEngine;
using System.Collections;

public class Cows : MonoBehaviour {

    public bool tamed = false;
    public int maxFood = 150;
    public int food = 50;
    public int leather = 50;
    public bool alive;
    public Vector3 target;
    public GameObject tamer = null;
    string t1 = "Cow1";
    string t2 = "Cow2";

    // Use this for initialization
    void Start () {
        alive = true;
        target = this.transform.position;
        tamer = null;
    }

    // Update is called once per frame
    void Update () {
        if (food <= 0 && leather <= 0)
        {
            Destroy(gameObject);
        }
        if (tamer != null)
        {
            if (tamer.GetComponent<CharacterStats>().teamNo == 1)
            {
                this.gameObject.tag = t1;
            }
            else 
            {
                this.gameObject.tag = t2;
            }
        }
        if (alive)
        {
            strole();
        }
        if (food > maxFood)
        {
            food = maxFood;
        }
        
	}

    void strole()
    {
        if (target == null)
        {
            target = this.transform.position;
        }
        int ran = UnityEngine.Random.Range(1, 200);
        if (tamer)
        {
            if (Vector3.Distance(this.transform.position, tamer.transform.position) >= 6)
            {
                ran = 1;
            }
        }
        if (ran == 1)
        {
            if (tamed)
            {
                target = (tamer.transform.position + (new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0)));
            }
            else
            {
                target += new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0);
            }
        }
        else if (ran <= 20)
        {
            if (tamed)
            {
                food += 1;
            }
            
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, target, (2 * Time.smoothDeltaTime));
    }

}
