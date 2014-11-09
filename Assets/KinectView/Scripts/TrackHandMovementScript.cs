using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class TrackHandMovementScript : MonoBehaviour {
	
	[SerializeField]
	private GameObject[] _pRHand;

	[SerializeField]
	private GameObject _BodyManager;
	
	private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
	private BodySourceManager _BodySourceManager;

	private bool _isGrabbed = false;

	// Use this for initialization
	void Start () {
		_BodySourceManager = _BodyManager.GetComponent<BodySourceManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		Kinect.Body[] data = _BodySourceManager.GetData();
		if (data == null)
		{
			return;
		}



//		data[0].


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

//	private GameObject CreateBodyObject(int trackingId)
//	{
//		GameObject rightHand = Instantiate(_pRHand[trackingId]); 
//		return rightHand;
//	}
}
