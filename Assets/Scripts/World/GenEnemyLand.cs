//using UnityEngine;
//using System.Collections;

//public class GenEnemyLand : MonoBehaviour {
	
//	int h;
//	int w;
//    System.Random rnd = new System.Random();
//    int ran;

//    void Start () {

//        WorldStats stats = gameObject.GetComponent<WorldStats>();
//        h = stats.worldHeight;
//        w = stats.worldWidth;
//        genRandomLand(w, h);

//    }

//	public void genRandomLand(int width, int height)
//	{
//		for (int i = 0; i < height; i++)
//        {
//			for (int j = 0; j < width; j++)
//            {
//                ran = rnd.Next(0, 10);
//                Vector3 pos = new Vector3 (j, i, 100);
//                GameObject tile;
//                if (ran == 1)
//                {
//                    tile = (GameObject)Instantiate(Resources.Load("WallBlock1"), pos, Quaternion.identity);
//                    tile.name = "Wall (" + j + "," + i + ")";
//                }
//                else if (ran == 5)
//                {
//                    tile = (GameObject)Instantiate(Resources.Load("WallBlock2"), pos, Quaternion.identity);
//                    tile.name = "Wall (" + j + "," + i + ")";
//                }
//			}
//		}

//    }



//    void checkSurrTiles(int x, int y)
//    {
//        //check tiles to left, bottom left and below and return what type they are
//        string left = check(x - 1, y);
//        string bottomLeft = check(x - 1, y - 1);
//        string bottom = check(x, y - 1);
//        string bottomRight = check(x + 1, y - 1);
//        string right = check(x + 1, y);
//        string topRight = check(x + 1, y + 1);
//        string top = check(x, y + 1);
//        string topLeft = check(x - 1, y + 1);
//        //check if each tile is not null
//        if (left != null)
//        {
//            string type = left;

//        }
//        if (bottomLeft != null)
//        {
//            string type = bottomLeft;

//        }
//        if (bottom != null)
//        {
//            string type = bottom;

//        }
//        if (bottomRight != null)
//        {
//            string type = bottomRight;

//        }
//        if (right != null)
//        {
//            string type = right;

//        }
//        if (topRight != null)
//        {
//            string type = topRight;

//        }
//        if (top != null)
//        {
//            string type = top;

//        }
//        if (topLeft != null)
//        {
//            string type = topLeft;

//        }
        
//    }

//    GameObject tileBeingChecked;

//    string check(int checkx, int checky)
//    {
//        tileBeingChecked = GameObject.Find("Tile (" + checkx + "," + checky + ")");
//        if (!tileBeingChecked)
//        {
//            return null;
//        }
//        else
//        {
//            return tileBeingChecked.GetComponent<AllTiles>().type;
//        }


//    }

//}
