﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBarPlayer2 : MonoBehaviour {
	private Image image;
	
	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		image.fillAmount = PlayerScoreKeeper.Health2;
	}
}