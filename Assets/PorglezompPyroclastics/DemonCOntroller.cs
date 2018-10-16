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
    public float speed;
    public GameObject projectile;
    public GameObject specialprojectile;
    public Transform shotpoint;
    public static int i = 0;
    // Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody>();

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
        }

    }


    private IEnumerator Randomjump()
    {
        while (true)

        {
            if (transform.position.z >= 25)
            {
        
                while (transform.position.z >= -10)

                {

                    yield return new WaitForSeconds(1.7f);
                    rb.AddForce(0, speed, 0, ForceMode.Impulse);
                    int x = Random.Range(0, 2);
                                                   if (x == 1)
                    {
                        Instantiate(specialprojectile, shotpoint.position, Quaternion.identity);
                        print("hero");
                        yield return new WaitForSeconds(0.4f);
                        rb.AddForce(0, 0, -speed, ForceMode.Impulse);
                    }
                    else
                    {
                        Instantiate(projectile, shotpoint.position, Quaternion.identity);
                        print("hero");
                        yield return new WaitForSeconds(0.4f);
                        rb.AddForce(0, 0, -speed, ForceMode.Impulse);
                    }
                }
            }
            else
            {
    
                yield return new WaitForSeconds(1.7f);
                rb.AddForce(0, speed, 0, ForceMode.Impulse);
                int x = Random.Range(0, 2);
                if (x == 1)
                {
                    Instantiate(specialprojectile, shotpoint.position, Quaternion.identity);
                    print("hero");
                    yield return new WaitForSeconds(0.4f);
                    rb.AddForce(0, 0, speed, ForceMode.Impulse);
                }
                else
                {
                    Instantiate(projectile, shotpoint.position, Quaternion.identity);
                    print("hero");
                    yield return new WaitForSeconds(0.4f);
                    rb.AddForce(0, 0, speed, ForceMode.Impulse);
                }

            }
        }



    }
}
