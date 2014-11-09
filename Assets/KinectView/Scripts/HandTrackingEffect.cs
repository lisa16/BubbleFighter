using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;
using Kinect = Windows.Kinect;

public class HandTrackingEffect : MonoBehaviour {
	public GameObject BodySourceManager = null;
	public GameObject basic, fire, ice, ultimate, block;

    private ulong _trackedId1, _trackedId2;

    private BodySourceManager _bodyManager;

	private float timeBA1, timeIce1, timeFire1, timeUlti1, timeBlock1;
	private float timeBA2, timeIce2, timeFire2, timeUlti2, timeBlock2;

	// Use this for initialization
	void Start ()
	{
		_trackedId1 = 0;
		_trackedId2 = 0;


	}
	
		// Update is called once per frame
	void Update () {
        if (null == this.BodySourceManager)
		{
			return;
		}

		_bodyManager = BodySourceManager.GetComponent<BodySourceManager>();
		if (null == _bodyManager)
		{
			return;
		}

		
		Kinect.Body[] data = _bodyManager.GetData();
		if (data == null)
		{
			return;
		}
		
		if(_trackedId1 == 0)
		{
			Kinect.CameraSpacePoint closetPerson = new Kinect.CameraSpacePoint() { Z = 10.0f };
			foreach(var body in data)
			{
				if (body == null)
				{
					continue;
				}
				
				Kinect.CameraSpacePoint bodyPos = body.Joints[Kinect.JointType.SpineBase].Position;
				if( (bodyPos.X > -1.0f && bodyPos.X < 0) 
				   && (bodyPos.Z > .5f && bodyPos.Z < 1.3f) )
				{
					if(body.IsTracked && bodyPos.Z < closetPerson.Z)
					{
						_trackedId1 = body.TrackingId;
//						
//						// set hand state tracking for this body
//						_bodyManager.GetBodyFrameSource().OverrideHandTracking(_trackedId);
					}
				}
			}
		}

		if(_trackedId2 == 0)
		{
			Kinect.CameraSpacePoint closetPerson = new Kinect.CameraSpacePoint() { Z = 10.0f };
			foreach(var body in data)
			{
				if(body == null)
				{
					continue;
				}
				Kinect.CameraSpacePoint bodyPos = body.Joints[Kinect.JointType.SpineBase].Position;
				if( (bodyPos.X > -1.0f && bodyPos.X < 1.0f)
				   && (bodyPos.Z > .5f && bodyPos.Z < 1.3f))
				{
					if(body.IsTracked && bodyPos.Z < closetPerson.Z && _trackedId1 != body.TrackingId)
					{
						_trackedId2 = body.TrackingId;
					}
				}
			}
		}


		
		// are we still tracking and if so, be sure we track the hand position?
		bool isTracking1 = false;
		bool isTracking2 = false;

		timeBA1 += Time.deltaTime; timeIce1+= Time.deltaTime; timeFire1+= Time.deltaTime; 
		timeUlti1+= Time.deltaTime; timeBlock1+= Time.deltaTime;

		timeBA2 += Time.deltaTime; timeIce2+= Time.deltaTime; timeFire2+= Time.deltaTime; 
		timeUlti2+= Time.deltaTime; timeBlock2+= Time.deltaTime;

		foreach(var body in data)
		{
			if (body == null)
			{
				continue;
			}
			
			if(body.IsTracked && _trackedId1 == body.TrackingId)
			{
				isTracking1 = true; 	// flag that we are still tracking the person
				
                // render particles if the right hand is closed
				if (body.HandRightState == Kinect.HandState.Closed
				    && body.HandLeftState == Kinect.HandState.Closed)
				{
					//Block
				}else if ((body.HandRightState == Kinect.HandState.Closed 
				           && body.HandLeftState == Kinect.HandState.Open) ||
				          (body.HandRightState == Kinect.HandState.Open
				 		   && body.HandLeftState == Kinect.HandState.Closed))
				{
					//Basic Attack
					if(body.HandRightState == Kinect.HandState.Closed)
					{
						if(timeBA1 > 0.5f)
						{
							createSpell(1, 0, 0, body);
							timeBA1 = 0;
						}

					}else if(body.HandLeftState == Kinect.HandState.Closed)
					{
						if(timeBA1 > 0.5f)
						{
							createSpell(1, 0, 1, body);
							timeBA1 = 0;
						}
					}

				}else if ((body.HandRightState == Kinect.HandState.Lasso 
				           && body.HandLeftState == Kinect.HandState.Open) ||
				          (body.HandRightState == Kinect.HandState.Open
				 && body.HandLeftState == Kinect.HandState.Lasso))
				{
					//Ice
					if(body.HandRightState == Kinect.HandState.Lasso)
					{
						if(timeIce1 > 3.0f)
						{
							createSpell(2, 0, 0, body);
							timeIce1 = 0;
						}
						
					}else if(body.HandLeftState == Kinect.HandState.Lasso)
					{
						if(timeIce1 > 3.0f)
						{
							createSpell(2, 0, 1, body);
							timeIce1 = 0;
						}
					}
				}else if ((body.HandRightState == Kinect.HandState.Lasso 
				           && body.HandLeftState == Kinect.HandState.Closed) ||
				          (body.HandRightState == Kinect.HandState.Closed
				 && body.HandLeftState == Kinect.HandState.Lasso))
				{
					//Fire
					if(body.HandRightState == Kinect.HandState.Lasso)
					{
						if(timeFire1 > 3.0f)
						{
							createSpell(3, 0, 0, body);
							timeFire1 = 0;
						}
						
					}else if(body.HandRightState == Kinect.HandState.Lasso)
					{
						if(timeFire1 > 3.0f)
						{
							createSpell(3, 0, 1, body);
							timeFire1 = 0;
						}
					}
				}else if (body.HandRightState == Kinect.HandState.Lasso 
				           && body.HandLeftState == Kinect.HandState.Lasso)
				{
					if(timeUlti1 > 6.0f)
					{
						createSpell(4, 0, 0, body);
						timeUlti1 = 0;
					}

				}else
				{
					//Standby
				}

			}
			if(body.IsTracked && _trackedId2 == body.TrackingId)
			{
				isTracking2 = true;

				if (body.HandRightState == Kinect.HandState.Closed
				    && body.HandLeftState == Kinect.HandState.Closed)
				{
					//Block
				}else if ((body.HandRightState == Kinect.HandState.Closed 
				           && body.HandLeftState == Kinect.HandState.Open) ||
				          (body.HandRightState == Kinect.HandState.Open
				 		   && body.HandLeftState == Kinect.HandState.Closed))
				{
					//Basic Attack
					if(body.HandRightState == Kinect.HandState.Closed)
					{
						if(timeBA2 > 0.5f)
						{
							createSpell(1, 1, 0, body);
							timeBA2 = 0;
						}
						
					}else if(body.HandLeftState == Kinect.HandState.Closed)
					{
						if(timeBA2 > 0.5f)
						{
							createSpell(1, 1, 1, body);
							timeBA2 = 0;
						}
					}
					
				}else if ((body.HandRightState == Kinect.HandState.Lasso 
				           && body.HandLeftState == Kinect.HandState.Open) ||
				          (body.HandRightState == Kinect.HandState.Open
				 		   && body.HandLeftState == Kinect.HandState.Lasso))
				{
					//Ice
					if(body.HandRightState == Kinect.HandState.Lasso)
					{
						if(timeIce2 > 3.0f)
						{
							createSpell(2, 1, 0, body);
							timeIce2 = 0;
						}
						
					}else if(body.HandLeftState == Kinect.HandState.Lasso)
					{
						if(timeIce2 > 3.0f)
						{
							createSpell(2, 1, 1, body);
							timeIce2 = 0;
						}
					}
				}else if ((body.HandRightState == Kinect.HandState.Lasso 
				           && body.HandLeftState == Kinect.HandState.Closed) ||
				          (body.HandRightState == Kinect.HandState.Closed
				 		   && body.HandLeftState == Kinect.HandState.Lasso))
				{
					//Fire
					if(body.HandRightState == Kinect.HandState.Lasso)
					{
						if(timeFire2 > 3.0f)
						{
							createSpell(3, 1, 0, body);
							timeFire2 = 0;
						}
						
					}else if(body.HandLeftState == Kinect.HandState.Lasso)
					{
						if(timeFire2 > 3.0f)
						{
							createSpell(3, 1, 1, body);
							timeFire2 = 0;
						}
					}
				}else if (body.HandRightState == Kinect.HandState.Lasso 
				          && body.HandLeftState == Kinect.HandState.Lasso)
				{
					if(timeUlti2 > 6.0f)
					{
						createSpell(4, 1, 0, body);
						timeUlti2 = 0;
					}
					
				}
				else
				{
					//Standby
				}
			}

		}
		
		// reset tracked person
		if(!isTracking1)
		{
			_trackedId1 = 0;

		}

		if(!isTracking2)
		{
			_trackedId2 = 0;
		}
	}


	private void createSpell(int i, int j, int k, Kinect.Body body) {

				Kinect.CameraSpacePoint position = new Kinect.CameraSpacePoint();
				if (k == 0) {
						position = body.Joints [Kinect.JointType.HandRight].Position;
				} else if (k == 1) {
						position = body.Joints [Kinect.JointType.HandLeft].Position;
				}
				GameObject moves = null;
				switch (i) {
				case 1:
						moves = (GameObject)Instantiate (basic);
						break;
				case 2:
						moves = (GameObject)Instantiate (ice);
						break;
				case 3:
						moves = (GameObject)Instantiate (fire);
						break;
				case 4:
						moves = (GameObject)Instantiate (ultimate);
						break;
				case 5:
						moves = (GameObject)Instantiate (block);
						break;
				}

				moves.transform.position = new Vector3 (position.X, position.Y, position.Z);

				if (j == 0 && i != 5) {
						moves.rigidbody.velocity = new Vector3 (50, 0, 0);
						moves.rigidbody.AddForce (2000, 0, 0);
				} else if (j == 1 && i != 5) {
						moves.rigidbody.velocity = new Vector3 (-50, 0, 0);
						moves.rigidbody.AddForce (-2000, 0, 0);
				}
		}
			

}
