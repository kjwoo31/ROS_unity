using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Geometry;
using RosMessageTypes.TurtlebotTeleop;

[RequireComponent(typeof(Rigidbody))]
public class TwistBaseController : MonoBehaviour
{
    // ROS Connector
    private ROSConnection ros;

    public string NodeName = "TurtlebotMove";

    public Rigidbody BaseRigidbody;

    public Vector3 commandVelocityLinear = Vector3.zero;
    public Vector3 commandVelocityAngular = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
	// Rigidbody
        if (BaseRigidbody == null)
        {
            BaseRigidbody = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        // Get ROS connection static instance
        ros = ROSConnection.instance;   

	// ROS subscribe
        ros.Subscribe<MTurtlebotTeleop>(NodeName, ExecuteRobotCommands);

        Vector3 deltaPosition = commandVelocityLinear * Time.fixedDeltaTime;
        deltaPosition = BaseRigidbody.transform.TransformDirection(deltaPosition);
        Quaternion deltaRotation = Quaternion.Euler(-commandVelocityAngular * Mathf.Rad2Deg * Time.fixedDeltaTime);

        BaseRigidbody.MovePosition(BaseRigidbody.position + deltaPosition);
        BaseRigidbody.MoveRotation(BaseRigidbody.rotation * deltaRotation);
    }

    void ExecuteRobotCommands(MTurtlebotTeleop data)
    {
        commandVelocityLinear.z = (float)data.linear.x;
        commandVelocityAngular.y = (float)data.angular.z;
    }
}
