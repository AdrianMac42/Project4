using UnityEngine;
using System.Collections;

public class AllTiles : MonoBehaviour {
    
    public string type;
    int x;
    int y;


    public AllTiles(int x, int y, string type){
        this.x = x;
        this.y = y;
        this.type = type;
        }
    
}
