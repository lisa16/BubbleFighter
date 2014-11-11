﻿using UnityEngine;
using System.Collections;

public class OnP2Hit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag == "Power1")
		{
			PlayerScoreKeeper.Score1 += 1;
			PlayerScoreKeeper.Health2 -= .01f;
		}
		else if(collision.gameObject.tag == "Power2")
		{
			PlayerScoreKeeper.Score1 += 5;
			PlayerScoreKeeper.Health2 -= .02f;
		}
		else if(collision.gameObject.tag == "Power3")
		{
			PlayerScoreKeeper.Score1 += 10;
			PlayerScoreKeeper.Health2 -= .025f;
		}
	}
}