using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour {

	private int MAX = 5; //Maximum number of hearts
	public int startingHearts = 5; // Starting number of hearts.
	public int currHP;
//	private int maxHealth;
	private int healthPerHeart = 1; // health per container, in the case that we make each container have more health
	private PlayerHealthController health;

	public Image[] healthImages; //Creating an array that holds the images for health.
	public Sprite[] healthSprites; // Creates an array that holds the sprites for health.

	// Use this for initialization
	void Start () {
		currHP = startingHearts * healthPerHeart; //If we choose to add more health per heart (ie. full heart to half heart to empty heart) this will make it easier
		//maxHealth = MAX * healthPerHeart;
		checkHealth (); // Updating the current health
	}

	void checkHealth(){
		for(int i = 0; i < MAX; i++) //While our i is less than the MAX health, loop through hearts.
		{
			if (startingHearts <= i) { //If the starting heart is less than I, then we disable the image.
				healthImages [i].enabled = false;
			} 
			else {
				healthImages [i].enabled = true;
			}
		}
		updateHearts ();

	}


	void updateHearts(){
		bool empty = false;
		int i = 0;

		foreach (Image image in healthImages) {
			if (empty) {
				image.sprite = healthSprites [0]; //Sets this image to the empty heart.
			//	Debug.Log("The heart is empty");
			} 
			else {
				i++;// Incrementing the i we created
				if(currHP >= i * healthPerHeart){ //If the heart is full
					image.sprite = healthSprites [healthSprites.Length - 1]; // Goes to the previous heart image in the array. In our case we just have two hearts. Full and empty.
				}
				else{
					int currentHeartHealth = (int) (healthPerHeart - (healthPerHeart * i - currHP));
					int healthPerImage = healthPerHeart / (healthSprites.Length - 1); // in the case that we have more health per heart.
					int imageIndex = currentHeartHealth/healthPerImage;
					image.sprite = healthSprites[imageIndex]; // Setting the sprite image.
					empty = true;
				}
			}

		}

	}

	public void loseHeart(int amount){ // For removing hearts.
		currHP -= amount;
		currHP = Mathf.Clamp (currHP, 0, startingHearts * healthPerHeart); // To make sure we never go below 0 and above the max amount of hearts.
		updateHearts ();

	}






}