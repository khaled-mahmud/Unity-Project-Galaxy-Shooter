using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	//variable for store enemyExplosionPrefab
	[SerializeField]
	private GameObject _enemyExplosionPrefab;

	[SerializeField]
	//variable for speed
	private float _speed = 5.0f;

	//To instantiate or declare an object from UIManager script class
	private UIManager _uiManager;

	[SerializeField]
	//To instantiate or declare an object from AudioClip class to hold an audio clip
	private AudioClip _clip;

	// Use this for initialization
	void Start () {
		//To assign the gameobject to the declared class type variables from Class UIManager
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

	}
	
	// Update is called once per frame
	void Update () {

		//move down
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		//when off the screen on the bottom
		//respawn back on top with a new x position between the bounds of the screen

		if (transform.position.y < -7)
		{
			float randomX = Random.Range(-7f, 7f);
			transform.position = new Vector3(randomX, 7, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Laser")
		{
			if (other.transform.parent != null)
			{
				Destroy(other.transform.parent.gameObject);
			}
			Destroy(other.gameObject);
			Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
			
			//function call to update the score display on UI from UIManager script 
			_uiManager.UpdateScore();

			//function call to play the audio clip at this point when this object destroy aka exploded
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

			Destroy(this.gameObject);
		}
		else if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();

			if (player != null)
			{
				player.Damage();
			}
			Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

			//function call to play the audio clip at this point when this object destroy aka exploded
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

			Destroy(this.gameObject);
		}
	}
}
