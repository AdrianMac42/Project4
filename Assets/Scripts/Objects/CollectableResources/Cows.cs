using UnityEngine;
using System.Collections;

public class Cows : MonoBehaviour {

    public bool tamed = false;
    public int maxFood = 150;
    public int food = 50;
    public int leather = 50;
    public bool alive;
    public Vector3 target;
    public CharacterStats tamer;

    // Use this for initialization
    void Start () {
        alive = true;
        target = this.transform.position;
    }

    // Update is called once per frame
    void Update () {
        if(alive)
        {
            strole();
        }
        if (food > maxFood)
        {
            food = maxFood;
        }
        if (food <= 0 && leather <= 0)
        {
            Destroy(gameObject);
        }
	}

    void strole()
    {
        if (target == null)
        {
            target = this.transform.position;
        }
        int ran = UnityEngine.Random.Range(1, 60);
        if (ran == 1)
        {
            if (tamed)
            {
                target = (tamer.gameObject.transform.position + (new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0)));
            }
            else
            {
                target += new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3), 0);
            }
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, target, (30 * Time.smoothDeltaTime));
    }

}
