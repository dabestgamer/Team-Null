using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

	public bool TurnOff = false; //Give the script the ability to turn off or not.
	public GameObject enemy1; //For holding the first enemy of which we will be instantiating.
	public GameObject enemy2; //For holding the second enemy of which we will be instantiating.
	public Transform enemyPos1;// Transform location of enemy 1.
	public Transform enemyPos2; // Transform location of enemy 2.
	public GameObject EnemyTrig;
	//public GameObject demonEnemy;

	//public GameObject player = null;
	//Vector3 playerPos;
	//Vector3 enemeyPos;



	// Use this for initialization
	void Start () {
		//playerPos = transform.position;
		//enemeyPos = new Vector3 (0,0,0);


	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			if (enemy1 != null && enemy2 != null) {
				Instantiate (enemy1, enemyPos1.position, enemyPos1.rotation);
				Instantiate (enemy2, enemyPos2.position, enemyPos2.rotation);
			} else if (enemy1 != null) {
				Instantiate (enemy1, enemyPos1.position, enemyPos1.rotation);
			} else if (enemy2 != null) {
				Instantiate (enemy2, enemyPos2.position, enemyPos2.rotation);
			} 
			else {
			}

			//Instantiate(enemy1, enemyPos1.position, enemyPos1.rotation);
			//Instantiate(enemy2, enemyPos2.position, enemyPos2.rotation);
			//gameObject.GetComponent<BoxCollider2D> ().enabled = false; //TURNS OFF THE PLAYERS COLLIDER INSTEAD OF THE TRIGGER.
			if (TurnOff == true) {
				EnemyTrig.GetComponent<BoxCollider2D> ().enabled = false;
			}
			//Vector3 enemypos = new playerpos ();
			//Debug.Log (playerPos);



		}

	}
} 