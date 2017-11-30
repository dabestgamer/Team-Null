using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Image black;
	public Animator anim;
	public string sceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		sceneName = SceneManager.GetActiveScene ().name;
	}

	void OnTriggerEnter2D(Collider2D other) //kills player or enemy on contact, if they fall out of the level
	{
		if (other.gameObject.tag == "Player")
		{
			StartCoroutine (Fade ());
		}
	}

	IEnumerator Fade()
	{
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		if (sceneName == "Test Level") {
			GlobalControl.Instance.clearItems ();
		}
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}
