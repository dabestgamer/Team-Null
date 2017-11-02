using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) //loads first level
		{
			SceneManager.LoadScene (2);
		}
		if (Input.GetButtonDown ("Cancel")) //quits game
		{
			Application.Quit ();
		}
	}
}
