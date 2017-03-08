using UnityEngine;
using System.Collections;

public class GenWorld : MonoBehaviour {
	
	int h;
	int w;

    GameObject tiles = new GameObject();
    System.Random rnd = new System.Random();

    

	public GenWorld(int width, int height)
	{
        tiles.name = "Tiles";

        for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++)
            {
                int ran = rnd.Next(1, 10);
                Vector3 pos = new Vector3 (j, i, 100);
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



}
