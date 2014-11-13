using UnityEngine;
using System.Collections;

public class OnPlayerHit : MonoBehaviour {

	[SerializeField]
	private string _playerHealth, _enemyPlayerScore;

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
			GlobalUtil.AddAmount(_playerHealth, -.01);
			GlobalUtil.AddAmount(_enemyPlayerScore, +1);
		}
		else if(collision.gameObject.tag == "Power2")
		{
			GlobalUtil.AddAmount(_playerHealth, -.02);
			GlobalUtil.AddAmount(_enemyPlayerScore, +5);
		}
		else if(collision.gameObject.tag == "Power3")
		{
			GlobalUtil.AddAmount(_playerHealth, -.025);
			GlobalUtil.AddAmount(_enemyPlayerScore, +10);
		}
	}
}
