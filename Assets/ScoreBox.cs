using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBox : MonoBehaviour {

	Text text;
	public string scoreText;

	// Use this for initialization
	void Start () {
		
		text = this.GetComponent<Text>(); //get the text component in the gameobject you assigned

	}
	
	// Update is called once per frame
	void Update () {
		text.text = string.Format(scoreText, PlayerScoreKeeper.Score1, PlayerScoreKeeper.Score2); //set the text in the text component
}
}
