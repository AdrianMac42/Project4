using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour {

    public List<Item> swords = new List<Item>();
    public List<Item> daggers = new List<Item>();
    public List<Item> bows = new List<Item>();
    public List<Item> larmor = new List<Item>();
    public List<Item> marmor = new List<Item>();
    public List<Item> harmor = new List<Item>();
    public List<Item> shields = new List<Item>();

    public int swordsNo;
    public int daggersNo;
    public int bowsNo;
    public int larmorNo;
    public int marmorNo;
    public int harmorNo;
    public int shieldsNo;


    private void Update()
    {
        swordsNo = swords.Count;
        daggersNo = daggers.Count;
        bowsNo = bows.Count;
        larmorNo = larmor.Count;
        marmorNo = marmor.Count;
        harmorNo = harmor.Count;
        shieldsNo = shields.Count;
    }
}
