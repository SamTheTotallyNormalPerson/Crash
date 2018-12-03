using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour {

    public GameObject Target;
    public float targetDistance;
    public float allowedDistance;
    public float speed;
    public RaycastHit Shot;
    public float run;


	void Update () {
        transform.LookAt(Target.transform);
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out Shot))
        {
            targetDistance = Shot.distance;
            if(targetDistance >= allowedDistance)
            {
                speed = run;
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
            }

            else
            {
                speed = 0;
            }
        }
	}
}
