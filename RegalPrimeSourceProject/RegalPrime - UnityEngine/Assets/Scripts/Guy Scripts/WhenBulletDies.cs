// RegalPrime 12-01-14 - WhenBulletDies.cs

// Added to the bullet so that it disables itself after a set period of time or when it hits a Ground tagged object

using UnityEngine;
using System.Collections;

public class WhenBulletDies : MonoBehaviour
{
	public float deathDelay;		// How long this object stays alive for

	void OnEnable()
	{
		StartCoroutine (DisableAfter(deathDelay));
	}

	IEnumerator DisableAfter(float Delay)
	{
		yield return new WaitForSeconds (Delay);
		gameObject.SetActive (false);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.tag == "Ground")	// If ground object hit, disable this bullet
		{
			StopAllCoroutines ();
			gameObject.SetActive (false);
		}
	}
}
