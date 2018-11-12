﻿using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer>().enabled = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Enemy")
		{
			Destroy(other.gameObject);
		}
	}

}
