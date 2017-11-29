using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public GameObject[] inventory = new GameObject[2]; //Creating an array to hold our items, we decided that we can only hold 2 items at a time.
	public GameObject droppedWeapon;
	private PlayerText playerText;

	public void Awake()
	{
		playerText =  gameObject.GetComponentInChildren<PlayerText>();
		
	}

	public void AddItem(GameObject item){

		bool itemAdded = false;
		bool duplicate = false;

		for (int i = 0; i < inventory.Length; i++) {
			if (inventory[i] != null && inventory [i].name == item.name) //Player cannot pick up another of an item they already have.
			{
				duplicate = true;
				//Debug.Log ("Duplicate item.");
				break;
			}

			if (inventory [i] == null) { // looking in each spot of the inventry until an empty spot is found.
				inventory[i] = item;
				//Debug.Log (item.name + " was added");
				playerText.weaponPickedUp(item);
				itemAdded = true;
				//Make the object de activate to similate object being picked up.
				item.SendMessage ("DoInteraction");
				break;
			}
		}
		//If the inventory is full
		if (itemAdded == false && duplicate == false) {
			Vector3 itemPos = item.transform.position;// Storing the current position of the item that is to be picked up.
			//inventory [1].SetActive (true); un comment this to get the object to re appear at original position when dropped.
			droppedWeapon = Instantiate(inventory[0], itemPos, Quaternion.Euler(0,180,0));// Istantiating the dropped object where the player is standing.
			droppedWeapon.name = inventory[0].name; //Changing the name of the Instantiated object(clone) to be the same name as the object in the inventory.
			droppedWeapon.SetActive(true);//Setting the active to true in order to show the Instantiated object.
			Destroy(inventory[0]);

			inventory [0] = item; // First item in the array of objects
			item.SendMessage("DoInteraction");
			//Debug.Log ("Inventory is full - Item was switched out with secondary weapon.");
		}



	}

	public void switchWeapons(GameObject item1, GameObject item2) // Function for switching the primary and secondary weapons.
	{

		GameObject item3 = null; // Temporary game object to hold an item in the inventory array.

		item3 = inventory[0]; // putting the weapon from the first slot into the temporary game object.
		inventory[0] = item2; // Setting the weapon in the second slot into the first slot.
		inventory[1] = item3; // setting the weapon in the temporary slot into the second slot.
	}

	public void reloadItem(GameObject item)
	{
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] == null) { // looking in each spot of the inventry until an empty spot is found.
				inventory[i] = item;
				Debug.Log (item.name + " was reloaded.");
				//Debug.Log (item.name + " was added");
				//Make the object de activate to similate object being picked up.
				break;
			}
		}
	}
}