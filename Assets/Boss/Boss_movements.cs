using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_movements : MonoBehaviour
{
    public Transform goal;
    public float accuracy;
    public float speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        this.transform.LookAt(goal.position);
        Vector3 direction = goal.position - this.transform.position;
        if (direction.magnitude > accuracy)
        {
            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}
