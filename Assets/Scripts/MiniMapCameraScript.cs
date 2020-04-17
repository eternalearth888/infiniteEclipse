using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraScript : MonoBehaviour
{

	public Transform player; //public reference to ourplayer

	// called after Update and FixedUpdate
	// only Update after our player has moved
	// use something similar for sun and moon 

	void LateUpdate()
	{
		Vector3 newPosition = player.position; // gra the current player position that has changed
		newPosition.y = transform.position.y; // change our current y position in the minimap to our new y position for the camera
		transform.position = newPosition; // now the positions match up

		// rotate minimap with the player
		transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);

	}
}
