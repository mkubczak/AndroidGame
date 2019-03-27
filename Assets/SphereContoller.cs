using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereContoller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
    
      Vector3 direction = Vector3.zero;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            direction = -Vector3.left;
        }
      if(Input.GetKey(KeyCode.DownArrow))
      {
         direction = Vector3.left;
      }
      if (Input.GetKey(KeyCode.LeftArrow))
    {
          direction = Vector3.forward;
      }
      if (Input.GetKey(KeyCode.RightArrow))
      {
          direction = -Vector3.forward;
      }
     rigidbody.AddTorque(direction * 25f);
	
		
	}
}
