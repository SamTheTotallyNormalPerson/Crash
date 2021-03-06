﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour { 

    public float speed;

    private int direction = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Target")
        {
            if (direction == 1)
                direction = -1;
            else
                direction = 1;
        }
    }
}
