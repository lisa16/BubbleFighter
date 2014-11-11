using UnityEngine;
using System.Collections;

//Depreciated!!!!
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
			GlobalUtil.Score1 += 1;
			GlobalUtil.Health2 -= .01f;
		}
		else if(collision.gameObject.tag == "Power2")
		{
			GlobalUtil.Score1 += 5;
			GlobalUtil.Health2 -= .02f;
		}
		else if(collision.gameObject.tag == "Power3")
		{
			GlobalUtil.Score1 += 10;
			GlobalUtil.Health2 -= .025f;
		}
	}
}
