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
	private int Dialoguecount = 0; // used for cyclying through the player dialogue
	private int ghostKillcount = 0;
	private int demonKillcount = 0;
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

		if (TalkTime % talkFrequency == 0 && Dialoguecount == 0) {
			textchange.text = "What was that noise?";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
			Dialoguecount++;
		}
		else if (TalkTime % talkFrequency == 0 && Dialoguecount == 1 ) {
			textchange.text = "I need to get out of here.";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
			Dialoguecount++;
		}
		else if (TalkTime % talkFrequency == 0 && Dialoguecount == 2) {
			textchange.text = "I won't stop until I get out of this place.";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
			Dialoguecount = 0;
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
			stopTime = time + waitTime + 70; // Extra 50 due to longer sentence

		}

		else if(item.name == "Scalpel"){
			textchange.text = "This looks good for some basic damage.";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime;
		}

		else if(item.name == "Bone Saw"){
			textchange.text = "This can cause some damage to those demons!";
			canvas.enabled = true;
			time = Time.frameCount;
			stopTime = time + waitTime +70;
		}

	}

	public void enemyKilled(string tag){
		if (tag == "Ghost") {
			if (ghostKillcount == 0) {
				textchange.text = "See you later!";
				canvas.enabled = true;
				time = Time.frameCount;
				stopTime = time + waitTime;
				ghostKillcount++;
			} 
			else if (ghostKillcount == 1) {
				textchange.text = "Go back to where you came from!";
				canvas.enabled = true;
				time = Time.frameCount;
				stopTime = time + waitTime+20;
				ghostKillcount++;
			}
			else if (ghostKillcount == 2) {
				textchange.text = "Take that you dumb ghost!";
				canvas.enabled = true;
				time = Time.frameCount;
				stopTime = time + waitTime;
				ghostKillcount=0;
			}
		}
		else if (tag == "ShadowDemon"){
			if (demonKillcount == 0) {
				textchange.text = "Be gone Demon!";
				canvas.enabled = true;
				time = Time.frameCount;
				stopTime = time + waitTime;
				demonKillcount++;
			} 
			else if (demonKillcount == 1) {
				textchange.text = "You've met your match!";
				canvas.enabled = true;
				time = Time.frameCount;
				stopTime = time + waitTime+20;
				demonKillcount++;
			}
			else if (demonKillcount == 2) {
				textchange.text = "Leave me alone!";
				canvas.enabled = true;
				time = Time.frameCount;
				stopTime = time + waitTime;
				demonKillcount=0;
			}
		}
	}
}