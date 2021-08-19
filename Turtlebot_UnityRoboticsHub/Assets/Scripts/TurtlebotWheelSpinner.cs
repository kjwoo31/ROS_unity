using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Geometry;
using RosMessageTypes.TurtlebotTeleop;

[RequireComponent(typeof(Rigidbody))]
public class TurtlebotWheelSpinner : MonoBehaviour
{
    // ROS Connector
    private ROSConnection ros;

    public string NodeName = "TurtlebotMove";

    public Transform LeftWheel;
    public Transform RightWheel;

    public float WheelRadius = 0.0f;
    private float WheelBase = 0.0f;

    private float leftWheelAngularVelocity;
    private float rightWheelAngularVelocity;

    // initialization
    public Vector3 localVelocity = Vector3.zero;
    public Vector3 localAngularVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (LeftWheel != null && RightWheel != null)
        {
            // TODO(sam): figure out a better way of finding wheel radius...
            WheelRadius = LeftWheel.position.y - transform.position.y;
            WheelBase = Vector3.Distance(LeftWheel.position, RightWheel.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get ROS connection static instance
        ros = ROSConnection.instance;   

	// ROS subscribe
        ros.Subscribe<MTurtlebotTeleop>(NodeName, ExecuteWheelCommands);

        leftWheelAngularVelocity = -(localVelocity.z - localAngularVelocity.y * WheelBase / 2.0f) / WheelRadius;
        rightWheelAngularVelocity = -(localVelocity.z + localAngularVelocity.y * WheelBase / 2.0f) / WheelRadius;

        float radiansToDegrees = 180/Mathf.PI;
        RightWheel.localRotation *= Quaternion.Euler(0, radiansToDegrees * rightWheelAngularVelocity * Time.deltaTime, 0);
        LeftWheel.localRotation *= Quaternion.Euler(0, radiansToDegrees * leftWheelAngularVelocity * Time.deltaTime, 0);
    }


    void ExecuteWheelCommands(MTurtlebotTeleop data)
    {

        localVelocity.x = (float)data.linear.x;
        localAngularVelocity.z = (float)data.angular.z;
    }
}
