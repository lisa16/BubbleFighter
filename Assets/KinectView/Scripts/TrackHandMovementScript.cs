//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using Kinect = Windows.Kinect;
//
//public class TrackHandMovementScript : MonoBehaviour {
//	
//	[SerializeField]
//	private GameObject[] _pRHand;
//
//	[SerializeField]
//	public GameObject _BodyManager;
//
//	[SerializeField]
//	public GameObject _HandEffect;
//
//	public GameObject card;
//	
//	private float time;
//
//	private bool isCreated = false;
//	
//	private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
//	private BodySourceManager _BodySourceManager;
//	private HandTrackingEffect _HandTrackingEffect;
//
//	// Use this for initialization
//	void Start () {
//		_BodySourceManager = _BodyManager.GetComponent<BodySourceManager> ();
//		_HandTrackingEffect = _HandEffect.GetComponent<HandTrackingEffect> ();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//				time += Time.deltaTime;
//				Kinect.Body[] data = _BodySourceManager.GetData ();
//				if (data == null) {
//						return;
//				}
//
//
//				foreach (var body in data) {
//						ulong _trackingId1 = _HandTrackingEffect.getTrackedId1 ();
//						ulong _trackingId2 = _HandTrackingEffect.getTrackedId2 ();
//						if (body == null)
//								return;
//
//						if (body.IsTracked) {
//								if (body.HandRightState == Kinect.HandState.Closed && body.TrackingId == _trackingId1
//										&& body.TrackingId == _trackingId2) {
//										if (isCreated == false && time > 1) {
//												isCreated = true;
//												time = 0;
//												Kinect.CameraSpacePoint bodyPos = body.Joints [Kinect.JointType.HandRight].Position;
//												CreateNewobject (bodyPos.X, bodyPos.Y, bodyPos.Z);
//										}
//								} else if (body.HandLeftState == Kinect.HandState.Closed && body.TrackingId == _trackingId2) {
//										if (isCreated == false && time > 1) {
//												isCreated = true;
//												time = 0;
//												Kinect.CameraSpacePoint bodyPos = body.Joints [Kinect.JointType.HandLeft].Position;
//												CreateNewobject (bodyPos.X, bodyPos.Y, bodyPos.Z);
//										}
//								} else
//				
//										isCreated = false;
//		
//						}
//				}
//		}
//
//
////
////		List<ulong> trackedIds = new List<ulong>();
////		foreach(var body in data)
////		{
////			if (body == null)
////			{
////				continue;
////			}
////			
////			if(body.IsTracked)
////			{
////				trackedIds.Add (body.TrackingId);
////			}
////		}
////		
////		List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
//
//
//		
//		// First delete untracked bodies
////		foreach(ulong trackingId in knownIds)
////		{
////			if(!trackedIds.Contains(trackingId))
////			{
////				Destroy(_Bodies[trackingId]);
////				_Bodies.Remove(trackingId);
////			}
////		}
//		
////		foreach(var body in data)
////		{
////			if (body == null)
////			{
////				continue;
////			}
////			
////			if(body.IsTracked)
////			{
////				if(!_Bodies.ContainsKey(body.TrackingId))
////				{
////					_Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
////				}
////				
//////				RefreshBodyObject(body, _Bodies[body.TrackingId]);
////			}
////		}
////	}
//
//
//	private void CreateNewobject(float posX, float posY, float posZ) {
//		GameObject newCard = (GameObject)Instantiate(card);
//		newCard.transform.position = new Vector3(posX, posY, posZ);
//		if (Camera.main.transform.position.x > newCard.transform.position.x) {
//						newCard.rigidbody.velocity = new Vector3 (50, 0, 0);
//						newCard.rigidbody.AddForce (2000, 0, 0);
//				} else {
//						newCard.rigidbody.velocity = new Vector3 (-50, 0, 0);
//						newCard.rigidbody.AddForce (-2000, 0, 0);
//				}
//		//			newCard.AddComponent(new Rigidbody())
//		if (Mathf.Abs (newCard.rigidbody.velocity.y) > 3 || Mathf.Abs(newCard.rigidbody.velocity.z) > 3) {
//					Destroy (newCard);
//				}
//
//		
//	}
//
////	private GameObject CreateBodyObject(int trackingId)
////	{
////		GameObject rightHand = Instantiate(_pRHand[trackingId]); 
////		return rightHand;
////	}
//}
