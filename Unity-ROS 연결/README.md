## rosbridge, ROS#의 역할

Unity, ROS 통신을 위해 rosbridge, ROS#(ros-sharp) 사용.
- rosbridge : Web socket 통신을 활용하여 JSON 형태로 Unity, ROS 사이에서 정보 전달.
- ROS# : .NET applications, 특히 Unity에서 rosbridge를 통해 ROS 커뮤니케이션을 하기 위해 만들어진 C# 패키지.  
(ROS의 rosbridge <-WebSocket Connection-> Unity의 ros connector)
