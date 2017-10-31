using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {

	Text textChange;                      // Reference to the Text component.
	public Inventory  inventory;		 //Refferencing the inventory on the player.
	public GameObject item1,item2;       // Referencing the items(game objects) in the players inventory.





	void Awake()
	{ 
		// Set up the reference.
		textChange = GetComponent<Text>();
		textChange.text = "Inventory: ";

	}
		
	void Update()
	{


		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory> ();// Gets the inventory of the object tagged as the player.
		item1 = inventory.inventory [0]; // Setting the game object item1 to the first game object in the inventory array.
		item2 = inventory.inventory [1]; // Setting the game objext item2 to the second game object in the inventory array.


		if (item1 == null) { //Displaying the primary and secondary items onto the UI.
			textChange.text = "Primary item: Empty \n";
			textChange.text += "Secondary item: Empty";
		} 
		else {
			textChange.text = "Primary item: " + item1.name + "\n";
			if (item2 == null) {
				textChange.text += "Secondary item: Empty";
			}
			else {
				textChange.text += "Secondary item:  " + item2.name;
			}

		}
	}
}