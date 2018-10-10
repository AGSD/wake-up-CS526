using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Boundary properties
[System.Serializable]
public class Boundary {
    public float xMin, xMax;
}

// Collision properties
[System.Serializable]
public class Collisions {
    public bool didCollide = false;
    public bool didFallFlat = false;
    public bool didCollideSideways = false;

    public bool didBump = false;
}

// HealthBar Damage properties
[System.Serializable]
public class DamageValues {
    public int tableStumbleDamage = 30;
    public int tripOverDamage = 20;
    public int tripSideWaysDamage = 10;
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
    protected Joystick joystick;

    Vector3 moveDirection = Vector3.zero;

    // Boundary reference
    public Boundary boundary;

    // Collisions reference
    public Collisions collisions;

    // EnableDisable reference
    public EnableDisableButtons enableDisable;

    // DamageValues reference
    public DamageValues damageValues;

    public float speed;
    public float speedXAxis;
    public float jumpSpeed;

    public float gravity;

    // Reference to the player's health.
    PlayerHealth playerHealth;

    // Reference to the player's progress
    PlayerProgress playerProgress;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        joystick = FindObjectOfType<Joystick>();

        playerHealth = GetComponent<PlayerHealth>();
        playerProgress = GetComponent<PlayerProgress>();
    }

    // Update is called once per frame
    void Update() {

        if(!playerHealth.isDead) {
            if (controller.isGrounded) { 

                if (collisions.didCollide || collisions.didBump) {
                    moveDirection = new Vector3(
                        (Input.GetAxis("Horizontal") + joystick.Horizontal) * speedXAxis,
                        0.0f,
                        0.0f
                    );
                }
                if (collisions.didFallFlat || collisions.didCollideSideways) {
                    moveDirection = Vector3.zero;
                }

                if (!collisions.didCollide && !collisions.didFallFlat && !collisions.didCollideSideways) {
                    moveDirection = new Vector3(
                        (Input.GetAxis("Horizontal") + joystick.Horizontal) * speedXAxis,
                        0,
                        speed
                    );
                }

                moveDirection = transform.TransformDirection(moveDirection);

                if ((Input.GetKey(KeyCode.UpArrow) || joystick.Vertical > 0.5f) && enableDisable.isUpArrowEnabled) {
                    animator.SetTrigger("Jump");
                    moveDirection.y = jumpSpeed;
                    moveDirection.z += speed * 15;

                }
                if ((Input.GetKey(KeyCode.DownArrow) || joystick.Vertical < -0.5f) && enableDisable.isDownArrowEnabled) {
                    animator.SetTrigger("Slide");
                    moveDirection.z += speed;
                }
            }
            else {
                moveDirection.x = (Input.GetAxis("Horizontal") + joystick.Horizontal) * speed;
                moveDirection.z = speed;
            }
            moveDirection.y -= gravity * Time.deltaTime;

            controller.Move(moveDirection * Time.deltaTime);

            // Trigger falling animation when player goes below ground level
            if(transform.position.y < 0) {
                animator.SetTrigger("Free Fall");

                playerHealth.healthSlider.value = 0;
                playerProgress.loadingBar.gameObject.SetActive(false);
            }
        }
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
            DamagePlayer("Stumble");
        }

        if (other.CompareTag("TripOver")) {
            animator.SetTrigger("Fall Flat");
            DamagePlayer("Fall Flat");
        }

        if (other.CompareTag("TripSideways")) {
            animator.SetTrigger("Stumble Sideways");
            DamagePlayer("Stumble Sideways");
        }

        /*if(other.CompareTag("TableStumble") || other.CompareTag("TripSideways")) {
            animator.SetTrigger("Bump");
        }*/
    }
    private void DamagePlayer(string animationTrigger) {
        // If the player has health to lose...
        if (playerHealth.currentHealth > 0) {
            // ... damage the player based on animation triggered.

            if(animationTrigger == "Stumble") {
                playerHealth.TakeDamage(damageValues.tableStumbleDamage);
            }
            else if(animationTrigger == "Fall Flat") {
                playerHealth.TakeDamage(damageValues.tripOverDamage);
            } else if(animationTrigger == "Stumble Sideways") {
                playerHealth.TakeDamage(damageValues.tripSideWaysDamage);
            }
        }
    }
}
