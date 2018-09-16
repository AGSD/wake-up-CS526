using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementBoundary {
    public float xMin, xMax, yMin, yMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private CapsuleCollider capCollider;
    private Animator animator;

    // For vertical movement (along z-axis)
    public float speed;

    // For clamping the player's horizontal movement (along x-axis)
    public MovementBoundary boundary;

    // For jumping
    public float jumpForce;
    public LayerMask groundLayers;

    void Start() {
        rb = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        // Forward (z-axis) Movement
        rb.AddForce((Vector3.forward * speed) * Time.deltaTime);
    }

    void Update() {
        // Left-Right lane-switch functionalities (Movement along x-axis)
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }

        // Jump Animation
        if(IsGrounded() && Input.GetKey(KeyCode.UpArrow)) {
            animator.SetTrigger("Jump");
        }

        // Slide Animation
        if(Input.GetKey(KeyCode.DownArrow)) {
            animator.SetTrigger("Slide");
        }

        // Avoid the player from falling off beyong the left and right walls
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
    }
    // Check to make sure not to go more up (by repetitive up-arrow clicks) once you're off the ground
    private bool IsGrounded() {
        return Physics.CheckCapsule(capCollider.bounds.center, 
                                    new Vector3(capCollider.bounds.center.x, capCollider.bounds.min.y, capCollider.bounds.center.z), 
                                    capCollider.radius * 0.9f, 
                                    groundLayers
                                   );
    }

    // Prevent the player from falling through the floor.
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Floor")) {
            rb.useGravity = false;
        }
    }
}
