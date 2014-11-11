using UnityEngine;
using System.Collections;

public class OnPlayerHit : MonoBehaviour {

	[SerializeField]
	private string _playerHealth, _enemyPlayerScore;

	// Use this for initialization
	void Start () {
		GlobalUtil.Put (_playerHealth, "100");
		GlobalUtil.Put (_enemyPlayerScore, "0");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag == "Power1")
		{
			GlobalUtil.Put(_playerHealth, "" + (int.Parse(GlobalUtil.Get (_playerHealth)) - 1));
			GlobalUtil.Put(_enemyPlayerScore, "" + int.Parse(GlobalUtil.Get (_enemyPlayerScore)) + 1);
//			PlayerScoreKeeper.Score2 += 1;
//			PlayerScoreKeeper.Health1 -= .01f;
		}
		else if(collision.gameObject.tag == "Power2")
		{
			GlobalUtil.Put(_playerHealth, "" + (int.Parse(GlobalUtil.Get (_playerHealth)) - 2));
			GlobalUtil.Put(_enemyPlayerScore, "" + int.Parse(GlobalUtil.Get (_enemyPlayerScore)) + 5);
//			PlayerScoreKeeper.Score2 += 5;
//			PlayerScoreKeeper.Health1 -= .02f;
		}
		else if(collision.gameObject.tag == "Power3")
		{
			GlobalUtil.Put(_playerHealth, "" + (int.Parse(GlobalUtil.Get (_playerHealth)) - 3));
			GlobalUtil.Put(_enemyPlayerScore, "" + int.Parse(GlobalUtil.Get (_enemyPlayerScore)) + 10);
//			PlayerScoreKeeper.Score2 += 10;
//			PlayerScoreKeeper.Health1 -= .025f;
		}
	}
}
