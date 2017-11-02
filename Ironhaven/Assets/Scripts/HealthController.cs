using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public Slider playerSlider;  //slider object to adjust slider bar for health
	public Image Fill;
	public GameObject player;
	private PlayerHealthController playerHP;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		playerHP = player.gameObject.GetComponent<PlayerHealthController> ();
	}

	// Update is called once per frame
	void Update () {

		//will check data holder for current health and update slider
		playerSlider.value = playerHP.currentHP;
		if (playerSlider.value <= playerHP.maxHP / 5) {
			Fill.color = Color.red;
		} else {
			Fill.color = Color.green;
		}
	}
} 