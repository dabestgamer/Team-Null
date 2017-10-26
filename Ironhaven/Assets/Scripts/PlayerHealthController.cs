using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Needed to be able reload a scene

public class PlayerHealthController : MonoBehaviour {

	public int maxHP = 5;
	public int currentHP = 0;

	// Use this for initialization
	void Start () {
		currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {

		if (currentHP > maxHP)
		{
			currentHP = maxHP;
		}

		//The following is only to test dealing damage, will be commented out for actual game
		if (Input.GetKeyDown ("q") && !this.transform.GetComponent<PlayerController>().hurt)
		{
			damageTest ();
		}
	}

	public void takeDamage(int damage) //inflicts damage to player
	{
		currentHP -= damage;
		if (currentHP <= 0) //player dies
		{
			currentHP = 0;
			playerDeath();
		}
	}

	void playerDeath() //reloads level on death, can be modified later to do a Game Over screen
	{
		SceneManager.LoadScene("Test Level");
	}

	void damageTest() //tests dealing 1 damage to player
	{
		this.transform.GetComponent<PlayerController> ().damage ();
		takeDamage (1);
	}
}
