using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Geometry;
using RosMessageTypes.TurtlebotTeleop;

public class UnityInputTeleop : MonoBehaviour
{
    // ROS Connector
    private ROSConnection ros;

    // Variables required for ROS communication
    public string TopicName = "cmd_vel";

    // Constants

    public float MaxForwardVelocity = 1.0f;
    public float MaxSidewaysVelocity = 1.0f;
    public float MaxRotationalVelocity = 3.0f;

    public float PublishingFrequency = 20.0f;

    public bool UseHolonomicControls = false;

    private MTurtlebotTeleop cmdVelMsg;

    /// <summary>
    /// Publish key board input as cmd_vel topic with PublishingFrequency (20Hz)
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        // Get ROS connection static instance
        ros = ROSConnection.instance;    
        cmdVelMsg = new MTurtlebotTeleop();
        StartCoroutine("PublishCommandVelocity");    
    }

    IEnumerator PublishCommandVelocity()
    {
        for (;;)
        {
            // Send the message to server_endpoint.py running in ROS
            ros.Send(TopicName, cmdVelMsg);
            yield return new WaitForSeconds(1.0f / PublishingFrequency);
        }
    }


    // Update is called once per frame
    void Update()
    {
        cmdVelMsg.linear.x = Input.GetAxis("Vertical") * MaxForwardVelocity;
        if (UseHolonomicControls)
        {
            cmdVelMsg.linear.y = -Input.GetAxis("Horizontal") * MaxSidewaysVelocity;
            cmdVelMsg.angular.z = -Input.GetAxis("Turning") * MaxRotationalVelocity;
        }
        else
        {
            cmdVelMsg.angular.z = -Input.GetAxis("Horizontal") * MaxRotationalVelocity;
        }        
    }

}
