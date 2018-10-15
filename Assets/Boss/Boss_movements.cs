using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_movements : MonoBehaviour
{
    public GameObject goal;
    public float accuracy;
    public float speed;
    public Slider HealthBar;
    Animator anim;
    private bool bossMoveFlag = true;
    public void setMoveBool(bool val) {
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking1", false);
        anim.SetBool("isAttacking2", false);
        bossMoveFlag = false;
    }
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossMoveFlag)
        {
            this.transform.LookAt(goal.transform.position);
            Vector3 direction = goal.transform.position - this.transform.position;
            if (direction.magnitude > accuracy)
            {
                this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
                anim.SetBool("isRunning", true);
                anim.SetBool("isAttacking1", false);
                anim.SetBool("isAttacking2", false);
            }
            else
            {
                anim.SetBool("isRunning", false);
                int number=Random.Range(1,3);
                anim.SetBool("isAttacking"+number, true);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HealthBar.value -= 1;
            Debug.Log("Bullet Hit");
            Destroy(other.gameObject, 0f);
        }

        if (HealthBar.value <= 0)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking1", false);
            anim.SetBool("isAttacking2", false);
            anim.SetBool("isDead", true);
            StartCoroutine(killPlayer(3));
        }

    }
    IEnumerator killPlayer(float seconds)
    {
        float currentTime = 0;
        bossMoveFlag = false;
        goal.GetComponent<Player_bossLevel_movements>().setMoveBool(false);
        
        while (currentTime < seconds)
        {
            Debug.Log("CO ROUTINE LOOP");
            currentTime += Time.deltaTime;
            yield return null;
        }
      //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
