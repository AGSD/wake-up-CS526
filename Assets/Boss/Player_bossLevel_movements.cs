using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bossLevel_movements : MonoBehaviour {
    public float speed = 10.0F;
    public float rotationSpeed = 500.0F;
    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
