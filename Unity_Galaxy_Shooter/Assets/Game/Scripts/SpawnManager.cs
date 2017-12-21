using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField]
	private GameObject enemyShipPrefab;

	[SerializeField]
	private GameObject[] powerups;

	//To instantiate or declare an object from GameManager script class
	private GameManager _gameManager;

	// Use this for initialization
	void Start () {

		//To assign the gameobject to the declared class type variables from Class GameManager
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		StartCoroutine(EnemySpawnRoutine());
		StartCoroutine(PowerUpSpawnRoutine());
	}

	public void StartSpawnRoutines()
	{
		StartCoroutine(EnemySpawnRoutine());
		StartCoroutine(PowerUpSpawnRoutine());
	}
	
	//create a coroutine to spawn the Enemy every 5 seconds

	IEnumerator EnemySpawnRoutine()
	{
		while (_gameManager.gameOver == false)
		{
			Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
			yield return new WaitForSeconds(5.0f);
		}
	}

	IEnumerator PowerUpSpawnRoutine()
	{
		while (_gameManager.gameOver == false)
		{
			int randomPowerup = Random.Range(0, 3);
			Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7, 7), 7, 0), Quaternion.identity);
			yield return new WaitForSeconds(5.0f);
		}
		
	}

}
