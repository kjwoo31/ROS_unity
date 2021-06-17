## ROS 공부 - [링크](http://wiki.ros.org/rospy_tutorials)

### 1. Create package in catkin workspace
```
cd ~/catkin_ws/src  
catkin_create_pkg beginner_tutorials std_msgs rospy roscpp  
cd ~/catkin_ws  
catkin_make  
. ~/catkin_ws/devel/setup.bash
```
- workspace에 대한 CMakeLists.txt, package.xml이 있고 각각의 package에 대한 CMakeLists.txt, package.xml 포함.
- description, maintainer 등 package.xml 구성 요소 수정.

### 2. Writing Publisher and Subscriber : 현재 시간을 String 형태로 주고받기
```
roscd beginner_tutorials  
# scripts 파일을 만들고 rospy로 publisher, subscriber 코드 작성. (talker.py)  
chmod +x talker.py
```

- rospy
```
#!/usr/bin/env python  
```
모든 python ROS node는 첫 줄에 이걸 표시한다.

- talker.py
```
pub = rospy.Publisher('chatter', String, queue_size=10)
```
이 node에서 chatter라는 topic을 String 형태로 publish한다는 뜻. queue_size로 subscriber에 쌓이는 메시지의 수를 제한.

```
rospy.init_node('talker', anonymous=True)
```
rospy에 node의 이름 전달. / 없이 base name을 적어야 하고 ROS Master와 통신하는 데 필요.

```
rate = rospy.Rate(10) # 10hz
rate.sleep() # while문 안에서
```
데이터 전송 주기 조절.

- listener.py
```
rospy.init_node('listener', anonymous=True)
rospy.Subscriber("chatter", String, callback)
rospy.spin()
```
- spin이 있으면 node가 shutdown되기 전까지 나가지 못하게 함.

### 3. Writing Service and Client : 2개의 int를 전송하면 그 합을 return
- srv 파일을 만들고 input, return 변수를 입력  

- add_two_ints_server.py  
```
s = rospy.Service('add_two_ints', AddTwoInts, handle_add_two_ints)  
```
AddTwoInts service type을 사용하는 add_two_ints 서비스를 정의. 모든 request는 handle_add_two_ints function을 지나간다.

- add_two_ints_client.py
```
rospy.wait_for_service('add_two_ints')
add_two_ints = rospy.ServiceProxy('add_two_ints', AddTwoInts)
```
init_node 대신 wait_for_service로 노드와 연결 후 service를 호출하는 handle 생성.

```
resp1 = add_two_ints(x, y)
return resp1.sum
```
만든 handle을 함수처럼 사용 가능.

### 4. 기타
1) parameter 불러오기
```
rospy.get_param('param_name')
```
roslaunch 파일에 적은 parameter를 불러온다.

2) log message 남기기
```
rospy.loginfo("I will publish to the topic %s", topic)
```
