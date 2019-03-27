using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform sphere;


	void Update () {

        transform.position = sphere.position + (new Vector3(0, 5f, - 5f));
        transform.LookAt(sphere);
	}
}
