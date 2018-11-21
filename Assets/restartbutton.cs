using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartbutton : MonoBehaviour {

	// Use this for initialization
    public void restart(){
        print("hero");
        SceneManager.LoadScene("Level 1 Boss");
    }
    public void levelup()
    {
        print("hero1");
        SceneManager.LoadScene("Level 2");
    }
}
