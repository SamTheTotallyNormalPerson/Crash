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
    public GameObject NitroBlowUp;
    public BoxCollider cratecollider;
    public GameObject masky;
    public bool IsNitro;
    public bool IsAkuAku;
    public bool IsTnt;
    public AudioSource TntSound;


    // Use this for initialization
    void Start () {

        

        NitroBlowUp.SetActive(false);

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

        if (IsNitro == true && CrateHealth == 0)
        {
            Destroy(CrateMesh1);
            Destroy(CrateMesh2);
            Destroy(cratecollider);
            NitroBlowUp.SetActive(true);

        }

        if (IsAkuAku == true && CrateHealth == 0)
        {
            Destroy(CrateMesh1);
            Destroy(CrateMesh2);
            Destroy(cratecollider);
            Instantiate(masky, transform.position, transform.rotation);
            Destroy(gameObject);

        }

        if (IsTnt == true && CrateHealth == 0)
        {
            Destroy(CrateMesh1);
            Destroy(CrateMesh2);
            Destroy(cratecollider);
            NitroBlowUp.SetActive(true);

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spin")
        {
            CrateHealth -= 1;
            crateDamnage.Play();

        }

        if (other.tag == "Player" && IsNitro == true)
        {
            CrateHealth -= 1;
            
        }

       if (other.tag == "Jump" && IsTnt == true)
        {
            Invoke("OnTntEnter", 4.5f);
            TntSound.Play();
        }

       else 


        if (other.tag == "Jump")
        {
            CrateHealth -= 1;
            crateDamnage.Play();

        }

       if (other.tag == "Death")
        {
            CrateHealth -= 1;
        }
    }

    void OnTntEnter()
    {
        CrateHealth -= 1;
    }
   
}

    
