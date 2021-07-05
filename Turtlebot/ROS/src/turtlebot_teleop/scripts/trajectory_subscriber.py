#!/usr/bin/env python
"""
    Subscribes to SourceDestination topic.
    Uses MoveIt to compute a trajectory from the target to the destination.
    Trajectory is then published to PickAndPlaceTrajectory topic.
"""
import rospy

from turtlebot_teleop.msg import TurtlebotTeleop


def callback(data):
    rospy.loginfo(rospy.get_caller_id() + "I heard:\n%s", data)

    # publish
    pub = rospy.Publisher('TurtlebotMove', TurtlebotTeleop, queue_size=10)

    r = rospy.Rate(20) # 20hz
    if not rospy.is_shutdown():
       pub.publish(data)
       r.sleep()

def listener():
    rospy.init_node('Trajectory_Subscriber', anonymous=True)
    rospy.Subscriber("SourceDestination", TurtlebotTeleop, callback)

    # spin() simply keeps python from exiting until this node is stopped
    rospy.spin()


if __name__ == '__main__':
    listener()
