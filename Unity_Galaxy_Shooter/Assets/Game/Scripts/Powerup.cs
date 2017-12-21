using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	[SerializeField]
	private float _speed = 3.0f;
	[SerializeField]
	private int powerupID;//0 = triple shot, 1 = speed boost, 2 = shields

	[SerializeField]
	//To instantiate or declare an object from AudioClip class to hold an audio clip
	private AudioClip _clip;

	// Update is called once per frame
	void Update () {

		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -7)
		{
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			//access the player
			//a handle to the component
			Player player = other.GetComponent<Player>();

			//Function call play the audio clip at this point when this object got picked up aka destroyed
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

			if (player != null)
			{
				
				if (powerupID == 0)
				{
					//enable triple shot here
					//if powerupid == 0
					player.TripleShotPowerOn();
				}
				else if (powerupID == 1)
				{
					//enable speed boost here
					player.SpeedBoostPowerOn();
				}
				else if (powerupID == 2)
				{
					//enable shields here
					player.EnableShields();
				}

			}

			//destroy ourselves
			Destroy(this.gameObject);
		}

	}
}
