using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poleFallingScript : MonoBehaviour {

	private GameObject player;
	private bool activated = false;
	public float distance;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		player = (GameObject) GameObject.FindGameObjectWithTag ("Player");
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!activated) {
			float playerz = player.transform.position.z;
			float polez = transform.position.z;
			if (playerz < polez && polez - playerz < distance) {
				rb.useGravity = true;
				activated = true;
			}
		}
	}
}
