﻿using UnityEngine;
using System.Collections;

public class WallCreator : MonoBehaviour {

	[SerializeField] GameObject m_Block;
	[SerializeField] float m_BlockSpeed;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.GetComponent<Bullet>() != null) {
			
			print ("Points colliding: " + other.contacts.Length);
			print ("First point that collided: " + other.contacts [0].point);

			GameObject blockCopy = Instantiate (m_Block);
			blockCopy.GetComponentInChildren<Block>().StretchBy (other.gameObject.GetComponent<Bullet> ().getChargeTime ());
			blockCopy.GetComponentInChildren<Block>().wallPoint = other.contacts[0].point;
			blockCopy.transform.position = other.contacts [0].point;


			// make the block forward direction the same as the wall's. This to help the block stretch in the right direction
			blockCopy.transform.localRotation = transform.localRotation;

			// destroying the bullet
			Destroy (other.gameObject);
		}
	}
}
