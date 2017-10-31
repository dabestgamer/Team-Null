using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	//the player transform. Can be modified in inspector. 
	public GameObject player;
	public float leftBound = -99999999999999; //prevents camera from going too far left
	public float rightBound = 99999999999999; //prevents camera from going too far right
	public float upperBound = 99999999999999; //prevents camera from going too high
	public float lowerBound = -99999999999999; //prevents camera from going too low

	//can set this to whatever value you want in inspector as you decide where exactly to place the camera. 
	//Note, in the update code the y-axis is turned off so camera will not change vertically. 
	public Vector3 cameraOffset;
	public float cameraX;
	public float cameraY;
	public float cameraZ;

	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag ("Player");
		cameraOffset.x = 0;
		cameraOffset.y = 2;
		cameraOffset.z = -5;
	}

	// Update is called once per frame
	void Update () {

		cameraX = player.transform.position.x + cameraOffset.x;
		cameraY = player.transform.position.y + cameraOffset.y;
		cameraZ = cameraOffset.z;

		if (cameraX < leftBound)
		{
			cameraX = leftBound;
		}
		else if (cameraX > rightBound)
		{
			cameraX = rightBound;
		}

		if (cameraY < lowerBound)
		{
			cameraY = lowerBound;
		}
		else if (cameraY > upperBound)
		{
			cameraY = upperBound;
		}

		//the camera will follow the player with a certain offset
		transform.position = new Vector3(cameraX, cameraY, cameraZ);
	}
}
