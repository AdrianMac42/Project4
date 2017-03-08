using UnityEngine;
using System.Collections;

public class GenGrassyWorld : MonoBehaviour {
	
	int h;
	int w;

    System.Random rnd = new System.Random();


    void Start ()
    {
        WorldStats stats = gameObject.GetComponent<WorldStats>();
        h = stats.worldHeight;
        w = stats.worldWidth;
        genGrassyWorld(w, h);
	}

	public void genGrassyWorld(int width, int height)
	{
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++)
            {
                int ran = rnd.Next(1, 10);
                Vector3 pos = new Vector3 (j, i, 101);
                GameObject tile;
                if (ran == 1)
                {
                    tile = (GameObject)Instantiate(Resources.Load("Grass1"), pos, Quaternion.identity);
                }
                else if (ran == 2)
                {
                    tile = (GameObject)Instantiate(Resources.Load("Grass2"), pos, Quaternion.identity);
                }
                else if (ran == 3)
                {
                    tile = (GameObject)Instantiate(Resources.Load("Grass3"), pos, Quaternion.identity);
                }
                else
                { 
                    tile = (GameObject)Instantiate(Resources.Load("Grass0"), pos, Quaternion.identity);
                }
				tile.name = "Tile ("+j+","+i+")";
			}
		}
	}



}
