using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {
    public GameObject play;
    // Use this for initialization
    public void pauseD()
    {
        gameObject.SetActive(false);
        play.SetActive(true);
        Time.timeScale = 0;
    }
}
