# Unity Robotics Hub Demo - [link](https://github.com/Unity-Technologies/Unity-Robotics-Hub)

### Part 0: ROS Setup
- ROS melodic과 package 설치, workspace 설정.

### Part 1: Create Unity scene with imported URDF
- Download Unity Editor(2020.2.0b9), set up a basic Unity Scene.
- Import a robot using URDF importer. (Niryo One, add package from [git](https://github.com/Unity-Technologies/URDF-Importer.git?path=/com.unity.robotics.urdf-importer))

### Part 2: ROS-Unity Integration
- TCP connection between Unity and ROS.
- Generating C# scripts from a ROS msg and srv files, and publishing to a ROS topic.
- 'ROS TCP Connector' 패키지를 사용하여 통신 가능. (ROS-Unity 통신 플러그인 사용, add package from [git](https://github.com/Unity-Technologies/ROS-TCP-Connector.git?path=/com.unity.robotics.ros-tcp-connector))
	- TcpConnector: contains ROSConnection, which provides the necessary functions to publish, subscribe, or call a service using the TCP endpoint ROS node.
	- MessageGeneration : generates C# classes, including serialization and deserialization functions, from ROS messages.


1. Niryo 메시지 종류
	- RobotTrajectory.msg
	- MNiryoMoveitJoints: a value for each joint in the Niryo arm as well as poses for the target object and target goal. 
	- MNiryoTrajectory: a list of RobotTrajectory values, which will hold the calculated trajectories for the pick-and-place task.
2. Niryo service: MMoverServiceRequest and MMoverServiceResponse describe the expected input and output formats for the service requests and responses when calculating trajectories.
3. Script
	- SourceDestinationPublisher: communicate with ROS, grabbing the positions of the target and destination objects and sending it to the ROS Topic "SourceDestination_input". (Unity's (x,y,z) is equivalent to the ROS (z,-x,y) coordinate)
		- Publish 버튼을 누르면 이 script의 Publish() 함수를 작동. 
		- roslaunch niryo_moveit part_2.launch를 실행하면 ROS로 정보 전달.


### Part 3: Pick-and-Place In Unity
<img src="https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/pick_and_place/img/4_old_flow.png" width="50%"></img>  
- run a pick-and-place task with known poses using MoveIt.
- creating and invoking a motion planning service in ROS, moving a Unity Articulation Body based on a calculated trajectory, and controlling a gripping tool to successfully grasp and drop an object.
	- TrajectoryPlanner: Where all of the logic to invoke a motion planning service lives, as well as the logic to control the gripper end effector tool.
		- PublishJoints
			- pickPoseOffset: 물체와의 충돌을 방지하기 위해 물체보다 살짝 높게 경로를 설정한다. (position = (target.transform.position + pickPoseOffset).To<FLU>())
			- CurrentJointConfig()로 joint 위치 가져옴. 
			- TrajectoryResponse()로 경로 가져옴. 
				- ExecuteTrajectories(): iterates through the joints to assign a new xDrive.target value based on the ROS service response, until the goal trajectories have been reached. Based on the pose assignment, this function may call the OpenGripper or CloseGripper methods as is appropriate.
	- mover.py: holds the ROS-side logic for the MoverService. When the service is called, the function plan_pick_and_place() runs. This calls plan_trajectory on the current joint configurations (sent from Unity) to a destination pose (dependent on the phase of the pick-and-place task).

  -------------------------------------------------
  
기타) Object Pose Estimation Demo - [link](https://github.com/Unity-Technologies/Robotics-Object-Pose-Estimation) (해보지는 않음)  
- Unity 환경 이미지 -> 집으려는 물체의 3D 위치를 예측. ([CNN 모델](https://github.com/Unity-Technologies/Robotics-Object-Pose-Estimation/tree/main/Model) 사용)
