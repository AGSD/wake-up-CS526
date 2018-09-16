using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax;
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

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {

        if(controller.isGrounded) {

            moveDirection = new Vector3(
                Input.GetAxis("Horizontal") * speedXAxis, 
                0, 
                speed
            );

            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetKey(KeyCode.UpArrow)) {
                animator.SetTrigger("Jump");
                moveDirection.y = jumpSpeed;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
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
        if(other.CompareTag("FlameRow") || other.CompareTag("FireBall") || other.CompareTag("LargeFlames")) {
            animator.SetTrigger("Die");
            Debug.Log("Collison with " + other.tag);
        }
    }
}
