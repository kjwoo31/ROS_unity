using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Constants
    public float MaxForwardVelocity = 1.0f;
    public float MaxSidewaysVelocity = 1.0f;
    public float MaxRotationalVelocity = 3.0f;

    public Rigidbody BaseRigidbody;

    public Vector3 commandVelocityLinear = Vector3.zero;
    public Vector3 commandVelocityAngular = Vector3.zero;

    public Transform LeftWheel;
    public Transform RightWheel;

    public float WheelRadius = 0.0f;
    private float WheelBase = 0.0f;

    private float leftWheelAngularVelocity;
    private float rightWheelAngularVelocity;

    // Start is called before the first frame update
    void Start()
    {
        commandVelocityLinear = Vector3.zero;
        commandVelocityAngular = Vector3.zero;   
 
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
        commandVelocityLinear.z = Input.GetAxis("Vertical") * MaxForwardVelocity;
        commandVelocityAngular.y = -Input.GetAxis("Horizontal") * MaxRotationalVelocity;

        leftWheelAngularVelocity = -(commandVelocityLinear.z - commandVelocityAngular.y * WheelBase / 2.0f) / WheelRadius;
        rightWheelAngularVelocity = -(commandVelocityLinear.z + commandVelocityAngular.y * WheelBase / 2.0f) / WheelRadius;

        float radiansToDegrees = 180/Mathf.PI;
        RightWheel.localRotation *= Quaternion.Euler(0, radiansToDegrees * rightWheelAngularVelocity * Time.deltaTime, 0);
        LeftWheel.localRotation *= Quaternion.Euler(0, radiansToDegrees * leftWheelAngularVelocity * Time.deltaTime, 0);  
    }

    void FixedUpdate()
    {

        Vector3 deltaPosition = commandVelocityLinear * Time.fixedDeltaTime;
        deltaPosition = BaseRigidbody.transform.TransformDirection(deltaPosition);
        Quaternion deltaRotation = Quaternion.Euler(-commandVelocityAngular * Mathf.Rad2Deg * Time.fixedDeltaTime);

        BaseRigidbody.MovePosition(BaseRigidbody.position + deltaPosition);
        BaseRigidbody.MoveRotation(BaseRigidbody.rotation * deltaRotation);      
  
    }
}
