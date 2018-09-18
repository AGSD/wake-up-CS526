using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax;
}

<<<<<<< HEAD
// collision properties
[System.Serializable]
public class Collisions {
    public bool didCollide = false;
    public bool didFallFlat = false;
    public bool didCollideSideways = false;
}

=======
>>>>>>> 72622a1c8c519f71e76dfeec06a125367190c067
public class PlayerMotor : MonoBehaviour {

    private Animator animator;
    private CharacterController controller;

    Vector3 moveDirection = Vector3.zero;

    public Boundary boundary;

    public float speed;
    public float speedXAxis;
    public float jumpSpeed;

    public float gravity;

<<<<<<< HEAD
    public Collisions collisions;


=======
>>>>>>> 72622a1c8c519f71e76dfeec06a125367190c067
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {

        if(controller.isGrounded) {

<<<<<<< HEAD
            if(collisions.didCollide) {
                moveDirection = new Vector3(
                    Input.GetAxis("Horizontal") * speedXAxis,
                    0,
                    0
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
=======
            moveDirection = new Vector3(
                Input.GetAxis("Horizontal") * speedXAxis, 
                0, 
                speed
            );
>>>>>>> 72622a1c8c519f71e76dfeec06a125367190c067

            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetKey(KeyCode.UpArrow)) {
                animator.SetTrigger("Jump");
                moveDirection.y = jumpSpeed;
                moveDirection.z += speed * 15;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                animator.SetTrigger("Slide");
                moveDirection.z += speed;
            }
<<<<<<< HEAD
        }
=======
         }
>>>>>>> 72622a1c8c519f71e76dfeec06a125367190c067
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
<<<<<<< HEAD
        if(other.CompareTag("TableStumble")) {
            animator.SetTrigger("Stumble");
        }

        if (other.CompareTag("TripOver")) {
            animator.SetTrigger("Fall Flat");
        }

        if (other.CompareTag("TripSideways")) {
            animator.SetTrigger("Stumble Sideways");
=======
        if(other.CompareTag("FlameRow") || other.CompareTag("FireBall") || other.CompareTag("LargeFlames")) {
            animator.SetTrigger("Die");
            Debug.Log("Collison with " + other.tag);
>>>>>>> 72622a1c8c519f71e76dfeec06a125367190c067
        }
    }
}
