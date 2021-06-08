## Gazebo와 비교한 Unity의 특징
-	**학습 속도가 빠르다.** Gazebo에서는 현실 속도의 2~3배보다 더 빠르게 학습할 수 없지만 Unity에서는 현실 속도의 100배까지 학습 가능하다.
-	여러 물체가 동시에 움직일 수 있다. **병렬 학습**이 가능하다.
-	Game Engine이라 **다양한 환경**을 구성하거나 불러올 수 있다. (Assets store, developer community 활용, Gazebo World file import는 불가능)
    -	다양한 Physics engine 사용 가능. 예: NVIDIA PhysX, AGX Dynamics (산업용에 사용하는 엔진. hydrodynamics, wind 관련 module tutorial이 있다.)
    - Human Animation을 추가하여 환경 안에서 사람이 이동 가능.
    - 중력과 같은 환경 요소를 바꾸기 쉬워 우주, 바다 등의 환경 구성 가능. (AirSim for air vehicle).
-	URDF import package로 로봇의 URDF model import 가능.
-	Sensor SDK로 RGB, RGB-D, Lidar 사용 가능.
-	ROSSharp로 ROS와 연결.
-	Continuous collision detection으로 **물체 간 충돌 방지**. (Gazebo에서는 통과하는 경우가 있다.)
-	Better rendering, more photorealism than Gazebo.

