using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax;
}

// collision properties
[System.Serializable]
public class Collisions {
    public bool didCollide = false;
    public bool didFallFlat = false;
    public bool didCollideSideways = false;
}

// Enable/Disable Buttons Properties
[System.Serializable]
public class EnableDisableButtons {
    public bool isUpArrowEnabled = true;
    public bool isDownArrowEnabled = true;
}

public class PlayerMotor : MonoBehaviour {

    private Animator animator;
    private CharacterController controller;

    Vector3 moveDirection = Vector3.zero;

    public Boundary boundary;

    public float speed;
    public float speedXAxis;
    public float jumpSpeed;

    public float gravity;

    public Collisions collisions;
    public EnableDisableButtons enableDisable;


    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {

        if(controller.isGrounded) {
			
            if(collisions.didCollide) {
                moveDirection = new Vector3(
                    Input.GetAxis("Horizontal") * speedXAxis,
                    0.0f,
                    0.0f
                );
            }
            if (collisions.didFallFlat || collisions.didCollideSideways) {
                moveDirection = Vector3.zero;
            }

            if(!collisions.didCollide && !collisions.didFallFlat && !collisions.didCollideSideways) {
                moveDirection = new Vector3(
                    Input.GetAxis("Horizontal") * speedXAxis,
                    0,
                    speed
                );
            }

            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetKey(KeyCode.UpArrow) && enableDisable.isUpArrowEnabled) {
                animator.SetTrigger("Jump");
                moveDirection.y = jumpSpeed;
                moveDirection.z += speed * 15;

            }
            if (Input.GetKey(KeyCode.DownArrow) && enableDisable.isDownArrowEnabled) {
                animator.SetTrigger("Slide");
                moveDirection.z += speed;
            }
        }
        else {
            moveDirection.x = Input.GetAxis("Horizontal") * speed;
            moveDirection.z = speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
        ClampPosition();
    }
    private void ClampPosition() {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            transform.position.y,
            transform.position.z
        );
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("TableStumble")) {
            animator.SetTrigger("Stumble");
        }

        if (other.CompareTag("TripOver")) {
            animator.SetTrigger("Fall Flat");
        }

        if (other.CompareTag("TripSideways")) {
            animator.SetTrigger("Stumble Sideways");
        }
    }
}
