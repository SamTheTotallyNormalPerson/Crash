using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public float CrateHealth;
    public int CrateVaule;
    public AudioSource crateDamnage;
    public float destoryTime;
    public GameObject CrateMesh1;
    public GameObject CrateMesh2;
    public BoxCollider cratecollider;
    // Use this for initialization
    void Start () {
        if (CrateHealth == 0)
        {
            FindObjectOfType<GameManager>().AddBox(CrateVaule);
            Destroy(gameObject);
            
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (CrateHealth == 0)
        {
            
            FindObjectOfType<GameManager>().AddBox(CrateVaule);
            Destroy(CrateMesh1);
            Destroy(CrateMesh2);
            Destroy(cratecollider);
            Destroy(gameObject, destoryTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spin")
        {
            CrateHealth -= 1;
            crateDamnage.Play();

        }
    }
}

    
