# Turtlebot Teleoperation in Unity

Implement of Turtlebot Teleoperation in Unity Environment using URDF Importer, ROS–Unity Integration from [Unity Robotics Hub](https://github.com/Unity-Technologies/Unity-Robotics-Hub).

Imported URDF files from [turtlebot_description](https://github.com/turtlebot/turtlebot/tree/melodic/turtlebot_description/urdf). Sending and receiving messages between ROS and Unity.

---

## How to Start
```
git clone https://github.com/kjwoo31/ROS_unity.git
```

---

## 1. ROS Setup
1. The provided files require the following packages to be installed. ROS Melodic users should run the following commands if the packages are not already present:

   ```
   sudo apt-get update && sudo apt-get upgrade
   sudo apt-get install python-pip ros-melodic-robot-state-publisher ros-melodic-moveit ros-melodic-rosbridge-suite ros-melodic-joy ros-melodic-ros-control ros-melodic-ros-controllers ros-melodic-tf2-web-republisher
   sudo -H pip install rospkg jsonpickle
   ```

2. Navigate to the `/PATH/TO/ROS_unity/Turtlebot/ROS` directory of this downloaded repo.
   - This directory will be used as the [ROS catkin workspace](http://wiki.ros.org/catkin/Tutorials/using_a_workspace).
   - built and source the ROS workspace

    ```
    cd /PATH/TO/ROS_unity/Turtlebot/ROS/
    catkin_make && source devel/setup.bash
    ```

3. The ROS parameters will need to be set to your configuration in order to allow the server endpoint to fetch values for the TCP connection, stored in `src/turtlebot_teleop/config/params.yaml`. From your ROS workspace, assign the ROS IP in this `yaml` file:

    ```bash
    echo "ROS_IP: $(hostname -I)" > src/turtlebot_teleop/config/params.yaml
    ```

    > Note: You can also manually assign this value by navigating to the `params.yaml` file and opening it for editing.

    ```yaml
    ROS_IP: <your ROS IP>
    ```

    e.g.

    ```yaml
    ROS_IP: 192.168.50.149
    ```

The ROS workspace is now ready to accept commands!

---

## 2. Unity Setup
1. Install [Unity Hub](https://unity3d.com/get-unity/download).

1. Download 2020 version of Unity: This project is tested in **2020.3.11f1**.

1. Click the "Add" button in the top right of the "Projects" tab on Unity Hub, and navigate to and select the Turtlebot directory within this cloned repository (`/PATH/TO/ROS_unity/Turtlebot`) to add the tutorial project to your Hub.

1. Click the newly added project to open it.
   - Click 'ignore' if an error message appears. (This happens because we didn't import .msg file yet)

1. In Unity, double click to open the `Assets/Scenes/EmptyScene` scene if it is not already open.

1. Import .msg file from `Robotics -> ROS Message Browser` from the top menu bar. Browse turtlebot_teleop package and build TurtlebotTeleop.msg file.
  - If Unity Robotics packages doesn't appear, follow the directions from [here](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/quick_setup.md).

1. Select `Robotics -> ROS Settings` from the top menu bar.
   - In the ROS Settings window, the `ROS IP Address` should be the IP address of your ROS machine (*not* the one running Unity).
   - Find the IP address of your ROS machine. In Ubuntu, open a terminal window, and enter `hostname -I`.

Now you are ready to communicate with ROS and Unity.

---

## Launch the file and click the Play button in Unity.
    ```
    roslaunch turtlebot_teleop move.launch
    ```
    - Use wasd or arrow keys to send message.

---

## Resources
- Setting up a ROS workspace:
   > Note: this tutorial has been tested with ROS Melodic.
   -  http://wiki.ros.org/ROS/Installation
   -  http://wiki.ros.org/ROS/Tutorials/InstallingandConfiguringROSEnvironment
   - http://wiki.ros.org/catkin/Tutorials/create_a_workspace

---

