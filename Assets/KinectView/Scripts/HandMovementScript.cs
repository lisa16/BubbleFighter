using UnityEngine;
using System.Collections;

public class HandMovementScript : MonoBehaviour {

	private bool _closedOrnot;
	private BodySourceView body;
	public GameObject BodySourceManager;
	private BodySourceManager _BodySourceManager;

	private GameObject RightHand; // I create variable

	void Start () {
		body = BodySourceManager.GetComponent<BodySourceView> ();
		}
	void Update ()
	{




		// If Right Hand is empty null it finding else it changing hand position
		if( RightHand == null)
		{
			RightHand = GameObject.Find("HandRight");

		}
		else
		{
			gameObject.transform.position = new Vector3( RightHand.transform.position.x,
			                                            RightHand.transform.position.y,
			                                            RightHand.transform.position.z
			                                            );

			if(body.isRightHandClosed() == true )
				transform.renderer.material.color = Color.white;
			else
				transform.renderer.material.color = Color.blue;
		}
	}
}
