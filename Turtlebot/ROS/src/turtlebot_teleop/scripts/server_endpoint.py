#!/usr/bin/env python

import rospy

from ros_tcp_endpoint import TcpServer, RosPublisher, RosSubscriber, RosService
from turtlebot_teleop.msg import TurtlebotTeleop

def main():
    ros_node_name = rospy.get_param("/TCP_NODE_NAME", 'TCPServer')
    tcp_server = TcpServer(ros_node_name)
    rospy.init_node(ros_node_name, anonymous=True)

    # Start the Server Endpoint with a ROS communication objects dictionary for routing messages
    tcp_server.start({
        'cmd_vel': RosPublisher('SourceDestination', TurtlebotTeleop, queue_size=10),
        'cmd_move': RosSubscriber('TurtlebotMove', TurtlebotTeleop, tcp_server),
#        'niryo_moveit': RosService('niryo_moveit', MoverService),
#        'niryo_one/commander/robot_action/goal': RosSubscriber('niryo_one/commander/robot_action/goal', RobotMoveActionGoal, tcp_server),
#        'sim_real_pnp': RosPublisher('sim_real_pnp', MoverServiceRequest)
    })
    rospy.spin()


if __name__ == "__main__":
    main()
