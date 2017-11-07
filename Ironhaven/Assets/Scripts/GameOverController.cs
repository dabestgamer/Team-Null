﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) //loads most recent level
		{
			SceneManager.LoadScene (PlayerPrefs.GetInt("LastLoadedLevel"));
		}
		if (Input.GetButtonDown ("Cancel")) //quits to title screen
		{
			SceneManager.LoadScene(0);
		}
	}
}