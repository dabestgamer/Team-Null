using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

	public bool inventory; // A boolean to let us know if the object can be stored in inventory



	public void DoInteraction(){
		//picked up and put in inventory
		gameObject.SetActive(false); //Causes the game object to dissapear on the push of the e key.
	}
		
}
