using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Vector3 offset;
    public GameObject player;
	public float lerpRate;

	// Use this for initialization
	void Start () {
		offset = player.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		Vector3 target = player.transform.position - offset;
		pos = Vector3.Lerp (pos, target, lerpRate * Time.deltaTime);
		transform.position = pos;
	}
}
