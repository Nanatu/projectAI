// RegalPrime 12-01-14 - Shooting.cs

// This script is attached to the player to allow him to shoot via the fire button
// Uses object pooling to recycle the bullets

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooting : MonoBehaviour
{
	public GameObject bulletPrefab;						// Attach the bullet you wish to fire (possibly add multi bullet later via array)

	public Transform  bulletOffset;						// Where the bullet fires from
	public int maxBullets = 0;							// Number of max bullets on the screen at a time (0 = infinite)
	public float fireRate = .1f;						// Delay inbetween firing
	public float bulletSpeed = 4f;						// Speed of the bullet
	public float deathTimer = 2f;
	public bool AutoFire = false;						// Enables rapidfire if true

	private bool ableToFire = true;						// Used in determining how often player can fire
	
	public AudioClip bulletSound;						// Default bullet sound

	private List<GameObject> BulletObjects = new List<GameObject>();	// List of bullets to reuse via object pooling
	private PlatformerCharacter2D PlayerScript;							// Reference to the player script

	void Start ()
	{
		// Make the bullets material the same as this objects
		bulletPrefab.GetComponent<SpriteRenderer>().material = gameObject.GetComponent<SpriteRenderer>().material;
		bulletPrefab.GetComponent<SpriteRenderer> ().sortingOrder = gameObject.GetComponent<SpriteRenderer> ().sortingOrder;

		PlayerScript = GetComponent<PlatformerCharacter2D> ();

		if(bulletOffset == null)	bulletOffset = gameObject.transform;

		EventManager.CreateEventManagerIfNeeded();
		EventManager.resetObjects += Reset;
	}
	void OnDestroy()
	{
		EventManager.resetObjects -= Reset;
	}
	void OnEnable()
	{
		EventManager.attackButton += OnButtonPressed_Shoot;
		EventManager.attackButton_Held += OnButtonPressed_Shoot_Held;
	}
	void OnDisable()
	{
		EventManager.attackButton -= OnButtonPressed_Shoot;
		EventManager.attackButton_Held -= OnButtonPressed_Shoot_Held;
	}
	void Reset()
	{
		ableToFire = true;
		foreach (GameObject child in BulletObjects)
		{
			child.SetActive (false);
			child.GetComponent<WhenBulletDies>().StopAllCoroutines();
		}
	}



	void OnButtonPressed_Shoot()
	{
		if(ableToFire && !PlayerScript.disableCharacterShooting)
		{
			ableToFire = false;
			Invoke ("ResetFire", fireRate);
			Shoot ();
		}
	}
	void OnButtonPressed_Shoot_Held()
	{
		if(ableToFire && !PlayerScript.disableCharacterShooting && AutoFire)
		{
			ableToFire = false;
			Invoke ("ResetFire", fireRate);
			Shoot ();
		}
	}



	public void Shoot()												// Create a bullet and make it move
	{
		GameObject clone = GetBulletClone ();

		if(clone != null)		// If able to get a bullet, fire it
		{
			if(PlayerScript.facingRight)
				clone.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
			else
				clone.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);

			MusicController.control.OneShotAudio (bulletSound);
		}
	}

	GameObject GetBulletClone()
	{
		foreach (GameObject child in BulletObjects)		// Check to see if there is a disabled bullet clone to reuse
		{
			if(!child.activeInHierarchy)
			{
				child.GetComponent<WhenBulletDies>().StopAllCoroutines();
				child.transform.position = bulletOffset.transform.position;
				child.SetActive (true);
				return child;
			}
		}

		if(BulletObjects.Count < maxBullets || maxBullets == 0)		// Create a new bullet clone if able
		{
			GameObject clone;
			clone = Instantiate (bulletPrefab, bulletOffset.transform.position, bulletOffset.transform.rotation) as GameObject;
			
			clone.tag = "Bullet";
			if (!clone.GetComponent<Rigidbody2D> ())
				clone.AddComponent<Rigidbody2D>().isKinematic = true;
			if(!clone.GetComponent<WhenBulletDies>())
				clone.AddComponent<WhenBulletDies>().deathDelay = deathTimer;
			BulletObjects.Add (clone);
			
			return clone;
		}

		return null;
	}

	void ResetFire()
	{
		ableToFire = true;
	}
}