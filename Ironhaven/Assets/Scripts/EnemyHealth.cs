using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public int currentHP;
	public int maxHP;
	private EnemyController enemy;
	public PlayerText Playertext;

	// Use this for initialization
	void Awake () {
		Playertext = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<PlayerText> ();
		enemy = GetComponent<EnemyController> ();
		if (enemy.tag == "Ghost")
		{
			maxHP = 5;
		}
		if (enemy.tag == "ShadowDemon")
		{
			maxHP = 7;
		}
		currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHP > maxHP)
		{
			currentHP = maxHP;
		}
	}

	public void addDamage(int damage) //damages enemy
	{
		currentHP -= damage;
		if (currentHP <= 0)
		{
			killEnemy ();
		}
	}
		
	void killEnemy() //kills enemy
	{
		//Debug.Log (Time.realtimeSinceStartup);
		Playertext.enemyKilled (gameObject.tag);
		//text.enemyKilled (gameObject.tag);
		Destroy (gameObject);

	}
}
