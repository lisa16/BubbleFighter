using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBox : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		
		text = this.GetComponent<Text>(); //get the text component in the gameobject you assigned

	}
	
	// Update is called once per frame
	void Update () {
		text.text = string.Format("Score: {0} {1} ", PlayerScoreKeeper.Score1, PlayerScoreKeeper.Score2); //set the text in the text component
}
}
