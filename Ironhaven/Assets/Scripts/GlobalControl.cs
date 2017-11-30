using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl Instance;

	public int sceneInt;
	public string sceneName;

	public GameObject item1;
	public GameObject item2;

	//each BGM to be played needs a clip and audio source

	public AudioClip titleScreenBGM;
	public AudioClip introTextBGM;
	public AudioClip tutorialBGM;
	public AudioClip levelOneBGM;
	public AudioClip levelTwoBGM;
	public AudioClip gameOverBGM;
	public AudioSource MusicSource;

	void Awake()
	{
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
		MusicSource.clip = titleScreenBGM;
		MusicSource.Play ();
	}

	void Update()
	{
		sceneName = SceneManager.GetActiveScene ().name;

		if (sceneName == "Title Screen" || sceneName == "End of Game") //clears inventory if player goes to title screen or beats the game
		{
			clearItems ();
		}

		//only plays the title screen theme on the title screen

		if (sceneName == "Title Screen") {
			musicChange (titleScreenBGM);
		}

		if (sceneName == "Game Over Screen") {
			musicChange (gameOverBGM);
		}

		if (sceneName.Contains ("Intro Text")) {
			musicChange (introTextBGM);
		}

		if (sceneName == "Test Level") {
			musicChange (tutorialBGM);
		}

		if (sceneName == "Level 1") {
			musicChange (levelOneBGM);
		}

		if (sceneName == "Level 2") {
			musicChange (levelTwoBGM);
		}

		if (sceneName == "End of Game") {
			MusicSource.Stop ();
		}
	}

	void musicChange(AudioClip bgm)
	{
		if (MusicSource.clip != bgm) {
			MusicSource.Stop ();
			MusicSource.clip = bgm;
			MusicSource.Play ();
		}
	}

	public void clearItems()
	{
		item1 = null;
		item2 = null;
	}
}
