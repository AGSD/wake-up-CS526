﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour {

    // Reference to the game's audio source
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // game is paused
        if(Mathf.Abs(Time.timeScale) < 0.00001) {
            audioSource.mute = true;
        }
        else {
            audioSource.mute = false;
        }
	}
}
