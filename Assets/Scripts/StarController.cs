using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour {
    public Transform defaultPosition;
    public float preferredHeight;
    public Transform[] relatedSpheres;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (relatedSpheres == null)
            return;

        Transform myTransform = GetComponent<Transform>();
        Rigidbody myRb = GetComponent<Rigidbody>();
        for (int i = 0; i < relatedSpheres.Length; i++)
        {
            Vector3 toOtherSphere = (relatedSpheres[i].position - myTransform.position);
            toOtherSphere.Normalize();
            myRb.AddForce(toOtherSphere);
        }
        myRb.AddForce(0, preferredHeight - myTransform.position.y, 0);
    }
}
