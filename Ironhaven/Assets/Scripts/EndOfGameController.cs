using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfGameController : MonoBehaviour {

	public Image black;
	public Animator anim;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) //loads first level
		{
			StartCoroutine(Fade(2));
		}
		if (Input.GetButtonDown ("Cancel")) //quits to title screen
		{
			StartCoroutine(Fade(0));
		}
	}

	IEnumerator Fade(int index)
	{
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (index);
	}
}
