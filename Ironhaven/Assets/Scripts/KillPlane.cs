using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour {

	// Use this for initialization
	private PlayerHealthController playerHP;
	private EnemyHealth enemyHP;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) //kills player or enemy on contact, if they fall out of the level
	{
//		Debug.Log ("Touched something!");
		if (other.gameObject.tag == "Player")
		{
			playerHP = other.gameObject.GetComponent<PlayerHealthController> ();
			playerHP.takeDamage (playerHP.maxHP);
		}
		if (other.gameObject.layer == 11)
		{
			enemyHP = other.gameObject.GetComponent<EnemyHealth> ();
			enemyHP.addDamage (enemyHP.maxHP);
		}
	}
}
