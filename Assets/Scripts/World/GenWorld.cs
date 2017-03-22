using UnityEngine;
using System.Collections;

public class GenWorld : MonoBehaviour {
	
	public int h = 100;
	public int w = 100;
    public int x;
    public int y;
    public GameObject tiles;
    System.Random rnd = new System.Random();
    
    public void genWorld(int width, int height)
	{
        x = width / 2;
        y = height / 2;

        for (int i = (0 - y); i < y; i++) {
			for (int j = (0 - x); j < x; j++)
            {
                int ran = rnd.Next(1, 10);
                Vector3 pos = new Vector3 (j, i, tiles.transform.position.z);
                GameObject tile;
                if (ran == 1)
                {
                    tile = (GameObject)Instantiate(Resources.Load("Grass1"), pos, Quaternion.identity);
                    tile.transform.parent = tiles.transform;
                }
                else if (ran == 2)
                {
                    tile = (GameObject)Instantiate(Resources.Load("Grass2"), pos, Quaternion.identity);
                    tile.transform.parent = tiles.transform;
                }
                else if (ran == 3)
                {
                    tile = (GameObject)Instantiate(Resources.Load("Grass3"), pos, Quaternion.identity);
                    tile.transform.parent = tiles.transform;
                }
                else
                { 
                    tile = (GameObject)Instantiate(Resources.Load("Grass0"), pos, Quaternion.identity);
                    tile.transform.parent = tiles.transform;
                }
				tile.name = "Tile ("+j+","+i+")";
			}
		}
	}

    private void Start()
    {
        genWorld(w, h);
    }

}
