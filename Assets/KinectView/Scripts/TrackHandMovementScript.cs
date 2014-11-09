using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class TrackHandMovementScript : MonoBehaviour {
	
	[SerializeField]
	private GameObject[] _pRHand;

	[SerializeField]
	private GameObject _BodyManager;

	public GameObject card;
	
	private float time;

	private bool isCreated = false;
	
	private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
	private BodySourceManager _BodySourceManager;

	// Use this for initialization
	void Start () {
		_BodySourceManager = _BodyManager.GetComponent<BodySourceManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		Kinect.Body[] data = _BodySourceManager.GetData();
		if (data == null)
		{
			return;
		}


		foreach (var body in data) 
		{
			if(body == null)
				return;
			if(body.IsTracked == true){
			if (body.HandRightState == Kinect.HandState.Closed) {
				if (isCreated == false && time > 1) {
					isCreated = true;
					time = 0;
					CreateNewobject (GameObject.Find ("HandRight").transform.localPosition);
				}
			} else
				isCreated = false;
			}
		}



		List<ulong> trackedIds = new List<ulong>();
		foreach(var body in data)
		{
			if (body == null)
			{
				continue;
			}
			
			if(body.IsTracked)
			{
				trackedIds.Add (body.TrackingId);
			}
		}
		
		List<ulong> knownIds = new List<ulong>(_Bodies.Keys);


		
		// First delete untracked bodies
//		foreach(ulong trackingId in knownIds)
//		{
//			if(!trackedIds.Contains(trackingId))
//			{
//				Destroy(_Bodies[trackingId]);
//				_Bodies.Remove(trackingId);
//			}
//		}
		
//		foreach(var body in data)
//		{
//			if (body == null)
//			{
//				continue;
//			}
//			
//			if(body.IsTracked)
//			{
//				if(!_Bodies.ContainsKey(body.TrackingId))
//				{
//					_Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
//				}
//				
////				RefreshBodyObject(body, _Bodies[body.TrackingId]);
//			}
//		}
	}


	private void CreateNewobject(Vector3 pos) {
		GameObject newCard = (GameObject)Instantiate(card);
		newCard.transform.position = new Vector3(pos.x, pos.y, pos.z);
		newCard.rigidbody.velocity = new Vector3(50, 0, 0);
		newCard.rigidbody.AddForce (2000, 0, 0);
		//			newCard.AddComponent(new Rigidbody())
		if (Mathf.Abs (newCard.rigidbody.velocity.y) > 5 || Mathf.Abs(newCard.rigidbody.velocity.z) > 5) {
					Destroy (newCard);
				}

		
	}

//	private GameObject CreateBodyObject(int trackingId)
//	{
//		GameObject rightHand = Instantiate(_pRHand[trackingId]); 
//		return rightHand;
//	}
}
