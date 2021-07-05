## 통신 방법

Unity, ROS 통신을 위해 ROS#(ros-sharp) 또는 Unity Robotics Hub 사용.
#### 1. ROS# : .NET applications, 특히 Unity에서 rosbridge를 통해 ROS 커뮤니케이션을 하기 위해 만들어진 C# package  
- non-Unity .NET application에 사용 가능
- WebSocket-connections via RosBridgeSuite : Web socket 통신을 활용하여 JSON, BSON 형태로 Unity, ROS 사이에서 정보 전달 (slow)
- Direct import and export of URDF models from ROS system

#### 2. Unity Robotics Hub: Tools, tutorials, resources, and documentation for robotic simulation in Unity
- Unity에서 사용할 수 있도록 간략화, 수정이 어려움.
- Direct TCP-based Communication: Direct communication of binary data (fast)
- ROS communication과 import URDF가 분리되어 따로 실행 (.urdf file)

