using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1Boss_PlayerMovement : MonoBehaviour
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

            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);


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
                    Instantiate(projectile, shotpoint.position, Quaternion.identity);
                    a--;
                    missiles.text = "Missiles Left :" + a;
                }
            }

            else
            {
                if (b > 0)
                {
                    Instantiate(projectile2, shotpoint.position, Quaternion.identity);
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

        if (transform.position.z>112){
            GetComponent<Collider>().isTrigger = true;
            Gameover.gameObject.SetActive(true);
        }

        if (transform.position.z < -86)
        {
            GetComponent<Collider>().isTrigger = true;
            Gameover.gameObject.SetActive(true);
        }


        float moveHorizontal = -Input.GetAxis("Horizontal");
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



   