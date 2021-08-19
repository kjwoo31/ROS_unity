using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using RosSharp.RosBridgeClient;

public class turtlebot_move : Agent
{
    Rigidbody rBody;
    void Start () {
        rBody = GetComponent<Rigidbody>();
    }
    public JoyAxisWriter[] joyAxisWriters;

    public Transform Target;

    public System.DateTime startTime;
    public System.DateTime currentTime;

    public override void OnEpisodeBegin()
    {
       // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < -1  || (currentTime-startTime).Seconds > 5 || (this.transform.localEulerAngles.x > 60 && this.transform.localEulerAngles.x < 300))
        {
	    for (int i = 0; i < 2; i++)
	        if (joyAxisWriters[i] != null)
	            joyAxisWriters[i].Write(0);
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3( 0, 0.01f, 0);
            this.transform.localRotation = Quaternion.identity;
	    // Initialize startTime (when Reached target, when end)
            startTime = System.DateTime.Now;
        }

        // Move the target to a new spot
        Target.localPosition = new Vector3(Random.value * 2 - 1,
                                           0.125f,
                                           Random.value * 2 - 1);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        float controlSignal_1 = 0;
        float controlSignal_2 = 0;
        controlSignal_1 = actionBuffers.ContinuousActions[0];
        controlSignal_2 = actionBuffers.ContinuousActions[1];
        joyAxisWriters[0].Write(controlSignal_1 * 2);
        joyAxisWriters[1].Write(controlSignal_2 * 2);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

	// Check currentTime
        currentTime = System.DateTime.Now;

	//Debug.Log((currentTime-startTime).Seconds);
	//Debug.Log(this.transform.localEulerAngles.x);

        // Reached target
        if (distanceToTarget < 0.355f)
        {
	    Debug.Log("Reached Target");
            SetReward(1.0f);
            EndEpisode();
        }
        // Fell off platform
        else if (this.transform.localPosition.y < -1 || (currentTime-startTime).Seconds > 5 || (this.transform.localEulerAngles.x > 60 && this.transform.localEulerAngles.x < 300))
        {
	    Debug.Log("Fail");
            EndEpisode();
        }
    }

}
