using UnityEngine;
using System.Collections;

public class Stockpile : MonoBehaviour {

    public int wood;
    public int stone;
    public int food;
    public int leather;


    public void subtractWood(int x)
    {
        wood = wood - x;
        //Debug.Log("Subtracted: " + x + " from Wood");
    }
    public void subtractStone(int x)
    {
        stone = stone - x;
        //Debug.Log("Subtracted: " + x + " from Stone");
    }
    public void subtractLeather(int x)
    {
        leather = leather - x;
        //Debug.Log("Subtracted: " + x + " from Leather");
    }

}
