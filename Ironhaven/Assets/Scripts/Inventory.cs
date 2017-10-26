using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public GameObject[] inventory = new GameObject[2]; //Creating an array to hold our items, we decided that we can only hold 2 items at a time.

	public void AddItem(GameObject item){

		bool itemAdded = false;

		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] == null) { // looking in each spot of the inventry until an empty spot is found.
				inventory[i] = item;
				Debug.Log (item.name + " was added");
				itemAdded = true;
				//Make the object de activate to similate object being picked up.
				item.SendMessage ("DoInteraction");
				break;
			}
		}
		//If the inventory is full
		if (itemAdded == false) {
			//inventory [1].SetActive (true); un comment this to get the object to re appear at original position when dropped.
			inventory [1] = item; // Second item in the array of objects
			item.SendMessage("DoInteraction");
			Debug.Log ("Inventory is full - Item was switched out with secondary weapon.");
		}



	}


}