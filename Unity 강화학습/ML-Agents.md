# ML-Agents tutorial - [link](https://github.com/Unity-Technologies/ml-agents/blob/release_17_docs/docs/Readme.md)

### 1. 설치
- 안전한 버전 설치
> git clone --branch release_17 https://github.com/Unity-Technologies/ml-agents.git
- Unity Package 'com.unity.ml-agents' install
- python으로 강화학습 - 'mlagents' package (anaconda 가상환경 제작 : mlagents)

### 2. tutorial 1 : 3D Balance Ball - [link](https://github.com/Unity-Technologies/ml-agents/blob/release_17_docs/docs/Getting-Started.md)
<img src="https://user-images.githubusercontent.com/59794238/121477798-3f42ff00-ca03-11eb-82f7-650e24e30a6e.gif" width="40%"></img>  

> conda activate mlagents  
> cd Desktop/ml-agents  
> mlagents-learn config/ppo/3DBall.yaml --run-id=first3DBallRun

- parallel training
- Behavior (observation space): Space Size of 8. the x and z components of the agent cube's rotation and the x, y, and z components of the ball's relative position and velocity.
- Actions: Space Size of 2. x and z rotations to apply to itself to keep the ball balanced on its head.
- Aborted (core dumped) 에러 발생 : torch 버전을 1.7.1에서 1.8.1로 바꾸어 해결. ('mlagents' package의 torch 사용)

### 3. Making a New Learning Environment - [link](https://github.com/Unity-Technologies/ml-agents/blob/release_17_docs/docs/Learning-Environment-Create-New.md)
> conda activate mlagents  
> cd Desktop/RollerBall  
> mlagents-learn config/rollerball_config.yaml --run-id=RollerBall
- 'TrainingArea' GameObject를 prefab으로 만들어 복제하면 parallel learning 가능.
	- parallel learning을 사용하면 학습 시간이 줄어듦. 성능에는 큰 차이 X. (step 줄여서 시간 재보기)
		- RollerBall 1개일 때: 50000 step을 하는데 697.590초 소요  
		<img src="https://user-images.githubusercontent.com/59794238/121476633-e1fa7e00-ca01-11eb-8802-fd07b6842183.png" width="30%"></img>  
		- RollerBall 3개일 때: 50000 step을 하는데 278.904초 소요  
		<img src="https://user-images.githubusercontent.com/59794238/121476667-ea52b900-ca01-11eb-8a10-b2fd0da87d03.png" width="30%"></img>  

### 4. Designing a Learning Environment - [link](https://github.com/Unity-Technologies/ml-agents/blob/release_17_docs/docs/Learning-Environment-Design.md)
- 건물 내부에서 길을 찾는 강화학습 환경 만들기 (turtlebot 예제처럼?)

