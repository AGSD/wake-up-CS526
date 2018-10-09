using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour {

    // References to the Transform of the UI Components in the RadialProgressBar gameObject.
    public Transform loadingBar;

    public Transform textIndicator;
    public Transform textLoading;

    // Values holding current progress and speed of progress.
    [SerializeField] private float currentProgress;
    [SerializeField] private float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(currentProgress < 100) {
            currentProgress += speed * Time.deltaTime;
            textIndicator.GetComponent<Text>().text = ((int)currentProgress).ToString() + "%";

            textLoading.gameObject.SetActive(true);
        }
        else {
            textIndicator.GetComponent<Text>().text = "DONE!";
            textLoading.gameObject.SetActive(false);
        }

        loadingBar.GetComponent<Image>().fillAmount = currentProgress / 100;
	}
}
