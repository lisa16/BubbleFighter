using UnityEngine;
using System.Collections;

public class OnP1Hit : MonoBehaviour {

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
			PlayerScoreKeeper.Score2 += 1;
			PlayerScoreKeeper.Health1 -= .01f;
		}
		else if(collision.gameObject.tag == "Power2")
		{
			PlayerScoreKeeper.Score2 += 5;
			PlayerScoreKeeper.Health1 -= .02f;
		}
		else if(collision.gameObject.tag == "Power3")
		{
			PlayerScoreKeeper.Score2 += 10;
			PlayerScoreKeeper.Health1 -= .025f;
		}
	}
}
