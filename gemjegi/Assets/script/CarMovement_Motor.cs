using UnityEngine;

public class CarMovement_Motor : MonoBehaviour
{
    public WheelJoint2D frontWheelJoint;
    public WheelJoint2D backWheelJoint;

    public float motorSpeed = -1000f; // 음수면 앞으로 감
    public float maxTorque = 1000f;

    void Update()
    {
        bool move = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        SetMotor(move);
    }

    void SetMotor(bool on)
    {
        JointMotor2D motor = new JointMotor2D
        {
            motorSpeed = motorSpeed,
            maxMotorTorque = maxTorque
        };

        frontWheelJoint.useMotor = on;
        backWheelJoint.useMotor = on;

        if (on)
        {
            frontWheelJoint.motor = motor;
            backWheelJoint.motor = motor;
        }
    }
}
