using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player_bossLevel_movements : MonoBehaviour {
    public GameObject boss;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public Slider HealthBar;
    public float speed = 10.0F;
    public float rotationSpeed = 500.0F;
    private Animator anim;
    private bool playerMoveFlag = true;

    public void setMoveBool(bool val) {
        playerMoveFlag = val;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            HealthBar.value -= 5;
            Debug.Log("HitSmall");
        }
        if (other.gameObject.tag == "Enemy2")
        {
            HealthBar.value -= 10;
            Debug.Log("HitBIG");
        }


        if (HealthBar.value <= 0) {
            anim.SetBool("isDead", true);
            StartCoroutine(killPlayer(3));
        }
       
    }
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}

    void Update()
    {
        if (playerMoveFlag)
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }

    }
    IEnumerator killPlayer(float seconds) {
        float currentTime = 0;
        playerMoveFlag = false;
        boss.GetComponent<Boss_movements>().setMoveBool(false);
       
        while (currentTime < seconds)
        {
            Debug.Log("CO ROUTINE LOOP");
            currentTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * 50;
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 5f);        
    }
}
