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
    public float currentProgress;
    [SerializeField] private float speed;

    // Free-Fall check to stop updating progress
    public bool isInFreeFall = false;

    // Use this for initialization
    void Start () {
        loadingBar.GetComponent<Image>().fillAmount = currentProgress;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(currentProgress < 100 && !isInFreeFall) {
            currentProgress += speed * Time.deltaTime;
            textIndicator.GetComponent<Text>().text = ((int)currentProgress).ToString() + "%";

            textLoading.gameObject.SetActive(true);
        }
        else if (currentProgress >= 100 && !isInFreeFall) {
            textIndicator.GetComponent<Text>().text = "DONE!";
            textLoading.gameObject.SetActive(false);
        }
        else if (isInFreeFall) {
            textIndicator.GetComponent<Text>().text = "YOU DIED!";
            textLoading.gameObject.SetActive(false);

            loadingBar.GetComponent<Image>().fillAmount = currentProgress;
        }

        if(!isInFreeFall) {
            loadingBar.GetComponent<Image>().fillAmount = currentProgress / 100;
        }
	}
}
