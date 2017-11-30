using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour {

	public Image black;
	public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) //loads first level
		{
			StartCoroutine (FadeToNextScene ());
		}
		if (Input.GetButtonDown ("Cancel")) //quits game
		{
			StartCoroutine (FadeToQuit ());
		}
	}

	IEnumerator FadeToNextScene()
	{
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (2);
	}

	IEnumerator FadeToQuit()
	{
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		Application.Quit ();
	}
}
