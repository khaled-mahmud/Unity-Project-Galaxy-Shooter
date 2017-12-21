using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To use the UI library of Unity Editor

public class UIManager : MonoBehaviour {

	//Variable decleration to store different types of life sprite sheets
	public Sprite[] lives;
	//Variable decleration to update the image display of player lives on UI
	public Image livesImageDisplay;
	//Variable decleration to store the score value for displaying on UI
	public int score;
	//Variable decleration to update the score display on UI
	public Text scoreText;

	//Declering variable to store instantiated object of gameobject Title
	public GameObject titleScreen;

	public void UpdateLives(int currentLives)
	{
		Debug.Log("Player lives " + currentLives);
		livesImageDisplay.sprite = lives[currentLives];
	}

	public void UpdateScore()
	{
		score += 10;

		scoreText.text = "Score: " + score;
	}

	public void ShowTitleScreen()
	{
		titleScreen.SetActive(true);
	}

	public void HideTitleScreen()
	{
		titleScreen.SetActive(false);
		scoreText.text = "Score: ";
	}
}
