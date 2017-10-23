using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int currentHP;
	public int maxHP;
	private EnemyController enemy;

	// Use this for initialization
	void Awake () {
		enemy = GetComponent<EnemyController> ();
		if (enemy.tag == "Ghost")
		{
			maxHP = 2;
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
		Destroy (gameObject);
	}
}
