using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBox : MonoBehaviour {

	private Text text;

	[SerializeField]
	private string _scoreText;

	// Use this for initialization
	void Start () {
		text = this.GetComponent<Text>(); //get the text component in the gameobject you assigned
	}
	
	// Update is called once per frame
	void Update () {
		text.text = string.Format(_scoreText, GlobalUtil.Get(GlobalUtil.PLAYER1SCORE), GlobalUtil.Get(GlobalUtil.PLAYER2SCORE)); //set the text in the text component
	}
}
