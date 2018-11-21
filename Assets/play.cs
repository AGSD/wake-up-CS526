using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour {

    public GameObject pause;
    // Use this for initialization
    public void played()
    {
        gameObject.SetActive(false);
        pause.SetActive(true);
        Time.timeScale = 1;
    }
}
