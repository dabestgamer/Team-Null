using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	public GameObject currentInterObj = null;
	public InteractionObject currentInterObjScript = null;
	public Inventory inventory;


	void Update()
	{
		if (Input.GetButtonDown ("Interact") && currentInterObj) { // The interact button is set to 'e' on the keyboard, 
			//Check if object can be stored in inventory.
			if (currentInterObjScript.inventory) {
				inventory.AddItem (currentInterObj);
			}

		}
	}
		

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag ( "InterObject")) {
			//Debug.Log (other.name);
			currentInterObj = other.gameObject;
			currentInterObjScript = currentInterObj.GetComponent <InteractionObject> ();

		}
	}



	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("InterObject")) {
			if(other.gameObject == currentInterObj){
				currentInterObj = null;    // When leaving the proximity of the inter obj, set it to null now. Since it is out of range.
			}
		}
	}

}