using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DemonCOntroller : MonoBehaviour
{
    public Text win;
    private Rigidbody rb;
    public Slider healthbar;
    public int health;
    public float jumpSpeed;
	public float moveSpeed;
    public GameObject projectile;
    public GameObject specialprojectile;
    public Transform shotpoint;
	public GameObject player;
    public static int i = 0;
	public bool enemyMove=true;
	Animator anim;
    // Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();

    }

    void Start()
    {

        healthbar.value = health;
        StartCoroutine(Randomjump());

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "aag ka gola")
        {
            healthbar.value = healthbar.value - 5;
        }
        if (other.gameObject.tag == "aag ka shola")
            healthbar.value = healthbar.value - 3;
    }
    // Update is called once per frame
    void Update()
    {

        if (healthbar.value<=0){
            win.gameObject.SetActive(true);
			enemyMove = false;


        }

    }




    private IEnumerator Randomjump()
    {
		while (enemyMove)
        {
			float curMoveSpeed = moveSpeed;
			Quaternion rotation = transform.rotation;

			if (transform.position.z >=  player.transform.position.z) {
				curMoveSpeed *= -1;
				rotation.y = -180;
			} else {
				rotation.y = 0;
			}
			transform.rotation = rotation;

			yield return new WaitForSeconds(1.7f);

			if(transform.position.y < 1.0)
				rb.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);

			int x = Random.Range(0, 2);


			if (x == 1)
				Instantiate (specialprojectile, shotpoint.position, rotation);
			else {
				GameObject newProjectile = Instantiate (projectile, shotpoint.position, rotation) as GameObject;
				newProjectile.transform.localScale = (new Vector3 (5f,5f,5f));
			}	
			if (transform.position.y < 1.0) {
					rb.AddForce (0, 0, curMoveSpeed, ForceMode.Impulse);
			}
        }
    }
}
