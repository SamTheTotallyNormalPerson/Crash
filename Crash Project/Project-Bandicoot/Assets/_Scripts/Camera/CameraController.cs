using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;

    public Vector3 offest;

    public bool useOffsetValues;

    public float rotatespeed;

    public Transform pivot;

    public float maxViewAngle;

    public float minViewAngle;

    public bool invertV;
	// Use this for initialization
	void Start () {
		if (!useOffsetValues)
        {
            offest = target.position - transform.position;
        }

        // pivot.transform.position = target.transform.position;
        // pivot.transform.parent = target.transform;
        pivot.transform.parent = null;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        pivot.transform.position = target.transform.position;

        //Get The X Position Of The Mouse And Rotate
        float horizontal = Input.GetAxis("Mouse X") * rotatespeed;
        pivot.Rotate(0, horizontal, 0);
        //Get The Y Position Of The Mouse And Rotate
        float vertical = Input.GetAxis("Mouse Y") * rotatespeed;
        pivot.Rotate(-vertical, 0, 0);
        if (invertV)
        {
            pivot.Rotate(vertical, 0, 0);
        } else
        {
            pivot.Rotate(-vertical, 0, 0);
        }
        //Limit X and Y Rotation
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //Move Camera
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offest);

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y -.5f, transform.position.z);
        }
        transform.LookAt(target);

	}
}
