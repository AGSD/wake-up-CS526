using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Text Gameover;
    public Text missiles;
    public Text bullets;
    Animator anim;
    private int a=10;
    private int b = 50;
    public static int j = 0;
    public GameObject projectile;
    public GameObject projectile2;
    public Transform shotpoint;
    public bool InAir = false;
    public float speed;
    private Rigidbody rb;
    public int jumpforce;
    public UnityEngine.UI.Slider healthbar1;
	private bool left;
	private Quaternion playerRotation;
	private Quaternion projectileRotation;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        missiles.text = "Missiles Left :" + a;
        bullets.text = "Bullets Left :" + b;
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
		left = true;
		playerRotation = Quaternion.identity;
		projectileRotation = Quaternion.identity;
		playerRotation.y = 180;
		projectileRotation.y = 0;

        healthbar1.value = 50;

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "fire" || other.gameObject.tag == "Enemy")
        {
            healthbar1.value = healthbar1.value - 10  ;
      
         
        }
        if (other.gameObject.tag == "Untagged" && InAir == true)
        {
            InAir = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Untagged" && InAir == true)
        {
            InAir = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        InAir = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyUp(KeyCode.R)){
            SceneManager.LoadScene("Sarthak Boss Battle");
        }

        if (healthbar1.value <= 0)
        {
            Gameover.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp("space"))
        {
            if (j == 0)
            {if (a > 0)
                {
					GameObject newProjectile = Instantiate(projectile, shotpoint.position, projectileRotation) as GameObject;
					newProjectile.transform.localScale = (new Vector3(0.3f,0.3f,0.3f));
                    a--;
                    missiles.text = "Missiles Left :" + a;
                }
            }

            else
            {
                if (b > 0)
                {
					GameObject newProjectile =  Instantiate(projectile2, shotpoint.position, projectileRotation) as GameObject;
					newProjectile.transform.localScale = (new Vector3(2.0f,2.0f,2.0f));
                    b--;
                    bullets.text = "Bullets Left :" + b;
                }
            }
        }


        if (Input.GetKeyUp("down")){
            if(j==0){
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                j++;

            }
            else
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                j--;

            }
        }

        /*if (transform.position.z>112){
            GetComponent<Collider>().isTrigger = true;
            Gameover.gameObject.SetActive(true);
        }

        if (transform.position.z < -86)
        {
            GetComponent<Collider>().isTrigger = true;
            Gameover.gameObject.SetActive(true);
        }*/

		float moveHorizontal = Input.GetAxis("Horizontal");


		//if (!Mathf.Approximately (moveHorizontal, 0)) {
			if (moveHorizontal < 0) {
				playerRotation.y = 180;
				projectileRotation.y = 0;
				left = true;
			} else if (moveHorizontal > 0) {
				playerRotation.y = 0;
				projectileRotation.y = 180;
				left = false;
			}
		//}

		transform.rotation = playerRotation;

		if(left)
        	transform.Translate(0, 0, -moveHorizontal * 12f * Time.deltaTime);
		else
			transform.Translate(0, 0, moveHorizontal * 12f * Time.deltaTime);

        if  (Input.GetKeyUp("up") && InAir == false)
        {
            anim.SetTrigger("jump");
            print("its working");
            Vector3 a = new Vector3(0, 8f, 0);
            transform.Translate(0, jumpforce*Time.deltaTime, 0);

        }


    }



}



   