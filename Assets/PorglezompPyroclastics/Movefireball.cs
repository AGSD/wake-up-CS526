using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movefireball : MonoBehaviour
{
    public int speed;
    public float timeline;
    public GameObject destroyobject;

    private int x = 0;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Des());

    }

    // Update is called once per frame
    void Update()
    {
        if (x == 2)
            x = 0;
        if (DemonCOntroller.i == 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            x++;
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            x++;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "hero"|| other.gameObject.tag == "aag ka gola" || other.gameObject.tag == "aag ka shola")
        {
            print("help");
            Instantiate(destroyobject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private IEnumerator Des()
    {
        while (true)
        {

            yield return new WaitForSeconds(1.5f);
            Instantiate(destroyobject, transform.position, Quaternion.identity);

            Destroy(gameObject);

        }
    }


}
