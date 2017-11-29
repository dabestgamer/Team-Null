using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour {
	public Canvas canvas;
	public Text textchange;
	private int time = 0; //Using the number of frames that have passed as a measure of time.
	private int stopTime = 0;// When the Canvas should be turned off from the view.
	public int waitTime = 0; //Adjustable amount of waiting time. (In frames)
	//private float stopTime;

	void Awake(){
		//time = Time.realtimeSinceStartup;
	}

	// Use this for initialization
	void Start () {
		textchange = GetComponent<Text> ();
		//textchange.text = "Hello";
		canvas.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		//time = Time.realtimeSinceStartup;
		//Debug.Log (Time.frameCount);
		//Debug.Log ("This is the time: " + time);
		//stopTime = time + 2;
		//Debug.Log ("This is the Stop Time: " + stopTime);
		time++;
		if (time == stopTime) {
			//Debug.Log ("The time should stop now");
			canvas.enabled = false;		
		}

		//textchange.text = "Hello there.";
		//enemyKilled();
	}

	public void weaponPickedUp(GameObject item){
		if (item.name == "Knife"){
			textchange.text = "I have a knife!";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;

		}

		else if(item.name == "Scalpel"){
			textchange.text = "I have a Scalpel!";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
		}

		else if(item.name == "Bone Saw"){
			textchange.text = "I have a Bone Saw!";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
		}

	}

	public void enemyKilled(string tag){
		if (tag == "Ghost") {
			textchange.text = "Take that you dumb Ghost!";
//			Debug.Log ("Ghost dialogue should pop up");
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;

		}
		else if (tag == "ShadowDemon"){
			textchange.text = "Bye bye Demon!";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
		}
	}
}