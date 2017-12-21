using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Variable decleration to datermine the state of the game
	public bool gameOver = true;

	//Declering variable to store instantiated object of gameobject Player
	public GameObject player;

	//To instantiate or declare an object from UIManager script class
	private UIManager _uiManager;

	private void Start()
	{
		//To assign the gameobject to the declared class type variables from Class UIManager
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}

	//if game over is true
	//if space key pressed
	//spawn the player
	//gameover is false
	//hide title screen

	private void Update()
	{
		if (gameOver == true)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Instantiate(player, Vector3.zero, Quaternion.identity);
				gameOver = false;
				
				//function call to update the title screen display on UI from UIManager script
				_uiManager.HideTitleScreen();
			}
		}
	}

}
