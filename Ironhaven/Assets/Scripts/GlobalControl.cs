using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl Instance;

	public GameObject item1;
	public GameObject item2;

	void Awake()
	{
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	void Update()
	{
		if (SceneManager.GetActiveScene ().buildIndex == 0) //clears inventory if player goes to title screen
		{
			item1 = null;
			item2 = null;
		}
	}
}
