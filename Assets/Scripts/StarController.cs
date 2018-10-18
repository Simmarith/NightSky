using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour {
    public Transform defaultPosition;
    public Transform otherSphere;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        Transform myTransform = GetComponent<Transform>();
        Vector3 toOtherSphere = (otherSphere.position - myTransform.position);
        toOtherSphere.Normalize();
        GetComponent<Rigidbody>().AddForce(toOtherSphere);
    }
}
