using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroTextController : MonoBehaviour {

	public Image black;
	public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) //loads first level
		{
			StartCoroutine (Fade ());
		}
	}

	IEnumerator Fade()
	{
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}
