using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

	//To instantiate or declare an object from Animator component from player prefab
	private Animator _anim;
	
	// Use this for initialization
	void Start () {

		//To assign the animation gameobject to the declared Animator type variables
		//to the gameObject which is Player prefab in which this script attached to
		//in short this gameObject aka this script
		_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//if a key or left arrow key is pressed down play Turn Left anim
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_anim.SetBool("Turn_Left", true);
			_anim.SetBool("Turn_Right", false);
		}
		//if a key or left arrow key is lifted up play Idle anim
		else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			_anim.SetBool("Turn_Left", false);
			_anim.SetBool("Turn_Right", false);
		}

		//if d key or right arrow key is pressed down play Turn right anim
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			_anim.SetBool("Turn_Right", true);
			_anim.SetBool("Turn_Left", false);
		}
		//if d key or right arrow key is lifted up play Idle anim
		else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
		{
			_anim.SetBool("Turn_Right", false);
			_anim.SetBool("Turn_Left", false);
		}

	}
}
