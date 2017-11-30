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
	private int TalkTime = 0;
	public int talkFrequency =0;
	private int count = 0; // used for cyclying through the player dialogue
	//private float stopTime;

	void Awake(){
		TalkTime = Time.frameCount;
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
		TalkTime = Time.frameCount;

		if (TalkTime % talkFrequency == 0 && count == 0) {
			textchange.text = "What was that noise?";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
			count++;
		}
		else if (TalkTime % talkFrequency == 0 && count == 1 ) {
			textchange.text = "I need to get out of here.";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
			count++;
		}
		else if (TalkTime % talkFrequency == 0 && count == 2) {
			textchange.text = "I won't stop until I get out of this place.";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
			count = 0;
		}
		 

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
			textchange.text = "Looks like I can hurt those flying creatures with this!";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime + 50; // Extra 50 due to longer sentence

		}

		else if(item.name == "Scalpel"){
			textchange.text = "This looks good for some basic damage.";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
		}

		else if(item.name == "Bone Saw"){
			textchange.text = "I can cause some damage to those demons with this!";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime +50;
		}

	}

	public void enemyKilled(string tag){
		if (tag == "Ghost") {
			textchange.text = "Take that you dumb Ghost!";
			//Debug.Log ("Ghost dialogue should pop up");
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