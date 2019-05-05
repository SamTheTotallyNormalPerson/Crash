using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuPickUo : MonoBehaviour {
    public GameObject AccessCrash;
    private Crash Crash;
	// Use this for initialization
	void Start () {
        Crash = AccessCrash.GetComponent<Crash>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Destroy(gameObject);
        }
    }
}
