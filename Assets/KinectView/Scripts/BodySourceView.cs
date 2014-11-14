using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    private bool _isRightHandClosed;

    private bool isCreated = true;

    private bool isPlayer1 = true;

	public GameObject P1Head, P2Head, P1Body, P2Body, P1Limb, P2Limb;
    private GameObject P1LimbLH, P2LimbLH, P1LimbRH, P2LimbRH, P1LimbLF, P2LimbLF, P1LimbRF, P2LimbRF;

    void Start()
    {
        P1Head = (GameObject)Instantiate(P1Head);
        P1Body = (GameObject)Instantiate(P1Body);
        P1LimbLH = (GameObject)Instantiate(P1Limb);
        P1LimbRH = (GameObject)Instantiate(P1Limb);
        P1LimbLF = (GameObject)Instantiate(P1Limb);
        P1LimbRF = (GameObject)Instantiate(P1Limb);
        P2Head = (GameObject)Instantiate(P2Head);
        P2Body = (GameObject)Instantiate(P2Body);
        P2LimbLH = (GameObject)Instantiate(P2Limb);
        P2LimbRH = (GameObject)Instantiate(P2Limb);
        P2LimbLF = (GameObject)Instantiate(P2Limb);
        P2LimbRF = (GameObject)Instantiate(P2Limb);
    }

    public bool isRightHandClosed()
    {
        return _isRightHandClosed;
    }

    public ulong _player1, _player2;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        //        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        //        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        //        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        //        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        //        
        //        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        //        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        //        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        //        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        //        
        //        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        //        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        //        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        //        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        //        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        //        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        //        
        //        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        //        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        //        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        //        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        //        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        //        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        //        
        //        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        //        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        //        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        //        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    void Update()
    {
        if (BodySourceManager == null)
        {
			Debug.LogError("BodySourceManager GameObject has not been initialized");
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
			Debug.LogError("BodySourceManager GameObject does not contain BodySourceManager Script");
            return;
        }

        Kinect.Body[] rawBodies = _BodyManager.GetData();
        if (rawBodies == null)
        {
            return;
        }

		List<ulong> trackedIds = GetTrackedIds (rawBodies);

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }
        isPlayer1 = true;

		foreach (Kinect.Body body in rawBodies)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body);
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);
                isPlayer1 = false;
            }
        }
    }

	private List<ulong> GetTrackedIds(Kinect.Body[] bodies)
	{
		List<ulong> trackingIds = new List<ulong>();
		foreach (var body in bodies)
		{
			if (body == null)
			{
				continue;
			}
			
			if (body.IsTracked)
			{
				trackingIds.Add(body.TrackingId);
				
//				if (body.HandRightState == Kinect.HandState.Closed)
//				{
//					_isRightHandClosed = true;
//					if (isCreated == false)
//						isCreated = true;
//				}
//				else
//				{
//					_isRightHandClosed = false;
//					isCreated = false;
//				}
			}
		}
		return trackingIds;
	}

    private GameObject CreateBodyObject(Kinect.Body bodyData)
    {
		ulong id = bodyData.TrackingId;
        GameObject body = new GameObject("Body:" + id);

        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            //            lr.SetVertexCount(2);
            //            lr.material = BoneMaterial;
            //            lr.SetWidth(0.05f, 0.05f);

            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
			jointObj.collider.enabled = false; //removing collider so it doesn't collide with our elements!
			Kinect.Joint joint;
			if(bodyData.Joints.TryGetValue(jt, out joint))
			{
				jointObj.renderer.material.color = BodySourceView.GetColorForState(joint.TrackingState);
			}
        }
        return body;
    }


    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {

        //		bool isP1 = isPlayer1;

        if (isPlayer1)
        {
            _player1 = body.TrackingId;
            PlayerTracking.player1TrackNum = body.TrackingId;
        }
        else
        {
            _player2 = body.TrackingId;
            PlayerTracking.player2TrackNum = body.TrackingId;
        }

        //		ulong player = body.TrackingId;
        //		Kinect.Joint? head = null;
        //		head = body.Joints [Kinect.JointType.Head];
        //		if(head !=null && head.HasValue)
        //		{
        //			var pos = head.Value.Position;
        ////			pos.X pos.Y pos.Z
        //		}
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {

            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.JointOrientation sourceJointOrientation = body.JointOrientations[jt];
            Kinect.Joint? targetJoint = null;

            if (_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }

            Transform jointObj = bodyObject.transform.FindChild(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint, isPlayer1, sourceJointOrientation);

            //			LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            //            if(targetJoint.HasValue)
            //            {
            //                lr.SetPosition(0, jointObj.localPosition);
            //				lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value, isPlayer1, sourceJointOrientation));
            //				lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            //            }
            //            else
            //            {
            //                lr.enabled = false;
            //            }
        }
    }

    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
            case Kinect.TrackingState.Tracked:
                return Color.green;

            case Kinect.TrackingState.Inferred:
                return Color.red;

            default:
                return Color.black;
        }
    }

    private Vector3 GetVector3FromJoint(Kinect.Joint joint, bool isPlayer1, Kinect.JointOrientation orien)
    {
        if (isPlayer1)
        {
            Vector3 newPos = new Vector3(-(joint.Position.Z * 10), joint.Position.Y * 10, joint.Position.X * 10);

            if (joint.JointType == Kinect.JointType.Head)
            {
                var hPos = joint.Position;
                P1Head.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
                var ori = orien.Orientation;
                P1Head.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
            }
            else if (joint.JointType == Kinect.JointType.SpineMid)
            {
                var hPos = joint.Position;
                P1Body.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
                var ori = orien.Orientation;
                P1Body.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
            }
            else if (joint.JointType == Kinect.JointType.HandLeft)
            {
                var lhandPos = joint.Position;
                P1LimbLH.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
                var ori = orien.Orientation;
                P1LimbLH.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
            }
            else if (joint.JointType == Kinect.JointType.HandRight)
            {
                var rhandPos = joint.Position;
                P1LimbRH.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
                var ori = orien.Orientation;
                P1LimbRH.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
            }
            else if (joint.JointType == Kinect.JointType.FootLeft)
            {
                var lfootPos = joint.Position;
                P1LimbLF.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
                var ori = orien.Orientation;
                P1LimbLF.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
            }
            else if (joint.JointType == Kinect.JointType.FootRight)
            {
                var rfootPos = joint.Position;
                P1LimbRF.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
                var ori = orien.Orientation;
                P1LimbRF.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
            }
            return newPos;
        }
        Vector3 newPos2 = new Vector3(joint.Position.Z * 10, joint.Position.Y * 10, joint.Position.X * 10);

        if (joint.JointType == Kinect.JointType.Head)
        {
            var hPos = joint.Position;
            P2Head.transform.position = new Vector3(newPos2.x, newPos2.y, newPos2.z);
            var ori2 = orien.Orientation;
            P2Head.transform.rotation = new Quaternion(ori2.X, ori2.Y, ori2.Z, ori2.W);
        }
        else if (joint.JointType == Kinect.JointType.SpineMid)
        {
            var hPos = joint.Position;
            P2Body.transform.position = new Vector3(newPos2.x, newPos2.y, newPos2.z);
            var ori2 = orien.Orientation;
            P2Body.transform.rotation = new Quaternion(ori2.X, ori2.Y, ori2.Z, ori2.W);
        }
        else if (joint.JointType == Kinect.JointType.HandLeft)
        {
            var lhandPos = joint.Position;
            P2LimbLH.transform.position = new Vector3(newPos2.x, newPos2.y, newPos2.z);
            var ori = orien.Orientation;
            P2LimbLH.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
        }
        else if (joint.JointType == Kinect.JointType.HandRight)
        {
            var rhandPos = joint.Position;
            P2LimbRH.transform.position = new Vector3(newPos2.x, newPos2.y, newPos2.z);
            var ori = orien.Orientation;
            P2LimbRH.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
        }
        else if (joint.JointType == Kinect.JointType.FootLeft)
        {
            var lfootPos = joint.Position;
            P2LimbLF.transform.position = new Vector3(newPos2.x, newPos2.y, newPos2.z);
            var ori = orien.Orientation;
            P2LimbLF.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
        }
        else if (joint.JointType == Kinect.JointType.FootRight)
        {
            var rfootPos = joint.Position;
            P2LimbRF.transform.position = new Vector3(newPos2.x, newPos2.y, newPos2.z);
            var ori = orien.Orientation;
            P2LimbRF.transform.rotation = new Quaternion(ori.X, ori.Y, ori.Z, ori.W);
        }
        return newPos2;
    }

    //	private static Vector3 GetVector3FromJointP2(Kinect.Joint joint)
    //	{
    //		return new Vector3(joint.Position.Z * 10, joint.Position.Y * 10, joint.Position.X * 10);
    //	}


}
