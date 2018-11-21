using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class restret : MonoBehaviour {

    public void restart()
    {
        print("hero");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1 Boss");
    }
}
