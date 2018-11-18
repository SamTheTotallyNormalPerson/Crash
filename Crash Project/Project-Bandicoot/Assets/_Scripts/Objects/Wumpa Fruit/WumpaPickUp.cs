using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WumpaPickUp : MonoBehaviour {

    public int value;
    public AudioSource wumpapickyupper;
    public float destoryTime = .12f;
    public GameObject wumpaMesh;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            wumpapickyupper.Play();
            FindObjectOfType<GameManager>().AddWumpa(value);
            Destroy(wumpaMesh);
            Destroy(gameObject , destoryTime);
        }
    }
}
