using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//variable to know if you collected the tripple shot power up
	public bool canTrippleShot = false;

	//variable to know if you collected the speed power up
	public bool isSpeedBoostActive = false;

	//variable to know if you collected shield power up
	public bool shieldsActive = false;

	//variable to know the lives of player
	public int lives = 3;

	//public or private identify
	//data type ( int, floats, bool, strings )
	//every variable has a NAME
	//option value assigned

	[SerializeField]
	private GameObject _explosionPrefab;
	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _trippleShot;
	[SerializeField]
	private GameObject _shieldGameObject;

	//Variable to hold the animations of engine failure
	[SerializeField]
	private GameObject[] _engines;
	

	//fireRate is 0.25
	//canfire -- has the amount of time between firing passed?
	//Time.time
	[SerializeField]
	private float _fireRate = 0.25f;

	private float _canFire = 0.0f;

	[SerializeField]
	private float _speed = 5.0f;

	//To instantiate or declare an object from UIManager script class
	private UIManager _uiManager;

	//To instantiate or declare an object from GameManager script class
	private GameManager _gameManager;

	//To instantiate or declare an object from SpawnManager script class
	private SpawnManager _spawnManager;

	//To instantiate or declare an object from AudioSource component from player prefab
	private AudioSource _audioSource;

	//Variable to track hitcount of player for engine failure animations
	private int hitCount = 0;
	
	void Start () {
		//current pos = new position
		transform.position = new Vector3(0, 0, 0);

		//To assign the gameobject to the declared class type variables from Class UIManager
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

		if (_uiManager != null)
		{
			_uiManager.UpdateLives(lives);
		}

		//To assign the gameobject to the declared class type variables from Class GameManager
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		//To assign the gameobject to the declared class type variables from Class SpawnManager
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

		if (_spawnManager != null)
		{
			_spawnManager.StartSpawnRoutines();
		}

		//To assign the audio gameobject to the declared AudioSource type variables
		//to the gameObject which is Player prefab in which this script attached to
		//in short this gameObject aka this script
		_audioSource = GetComponent<AudioSource>();

		hitCount = 0;

	}
	
	// Update is called once per frame
	void Update () {

		Movement();

		//if space key pressed
		//spawn laser at player position

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
		{
			Shoot();
		}

	}

	private void Shoot()
	{
		//if trpple shot
		//shoot 3 lasers
		//else
		//shoot 1

		if (Time.time > _canFire)
		{
			//function call to play the audio when shoot aka whe instantiate laser
			_audioSource.Play();

			if (canTrippleShot == true)
			{
				Instantiate(_trippleShot, transform.position, Quaternion.identity);
			}
			else
			{
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
			}
			
			_canFire = Time.time + _fireRate;
		}
	}

	private void Movement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		//if speed boost enabled
		//move 1.5x the normal speed
		//else
		//move normal speed

		if (isSpeedBoostActive == true)
		{
			transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
			transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
		}
		else
		{
			transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
			transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
		}


		//if player on the y is greater than 0
		//set player position on the Y to 0

		if (transform.position.y > 0)
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		else if (transform.position.y < -4.2f)
		{
			transform.position = new Vector3(transform.position.x, -4.2f, 0);
		}


		//if player position on the x is greater than 9.5
		// position on the x needs to be -9.5

		if (transform.position.x > 9.5f)
		{
			transform.position = new Vector3(-9.5f, transform.position.y, 0);
		}
		else if (transform.position.x < -9.5f)
		{
			transform.position = new Vector3(9.5f, transform.position.y, 0);
		}
	}

	//method to control Player damage
	public void Damage()
	{
		//subtract 1 life from the player
		//if player has shields
		//do nothing

		if (shieldsActive == true)
		{
			shieldsActive = false;
			_shieldGameObject.SetActive(false);
			return;
		}

		hitCount++;

		if (hitCount == 1)
		{
			//turn left engine failure on
			_engines[0].SetActive(true);
		}
		else if (hitCount == 2)
		{
			//turn right engine failure on
			_engines[1].SetActive(true);
		}

		lives--;

		//To update the UpdateLives function for UIManager Script class
		_uiManager.UpdateLives(lives);

		//if lives < 1 (meaning 0)
		//destroy this object
		if (lives < 1)
		{
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

			//Function call to start the game again after player died
			_gameManager.gameOver = true;

			//Function call to show the title screen after player died
			_uiManager.ShowTitleScreen();

			Destroy(this.gameObject);
		}
	}

	//method to enable to tripple shot
	public void TripleShotPowerOn()
	{
		canTrippleShot = true;
		StartCoroutine(TripleShotPowerDownRoutine());
	}

	//method to enable shield powerup
	public void EnableShields()
	{
		shieldsActive = true;
		_shieldGameObject.SetActive(true);
	}

	//method to enable to speed powerup
	public void SpeedBoostPowerOn()
	{
		isSpeedBoostActive = true;
		StartCoroutine(SpeedBoostDownRoutine());
	}

	//coroutine method (ienumerator) to power down the tripple shot
	public IEnumerator TripleShotPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		canTrippleShot = false;
	}

	//coroutine method (ienumerator) to power down the speed boost
	public IEnumerator SpeedBoostDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		isSpeedBoostActive = false;
	}


}
