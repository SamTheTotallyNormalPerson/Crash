using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currentWumps;
    public Text wumpatext;
    public int currentBox;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddWumpa(int wumptoadd)
    {
        currentWumps += wumptoadd;
        wumpatext.text = "" + currentWumps;
    }

    public void AddBox (int boxtoadd)
    {
        currentBox += boxtoadd;
    }
}
