using UnityEngine;

public class CarMovement_Motor : MonoBehaviour
{
    public carmovement carMovement;

    public WheelJoint2D frontWheelJoint;
    public WheelJoint2D backWheelJoint;

    private void Start()
    {
        if (carMovement == null)
        {
            carMovement = GetComponent<carmovement>();
        }
    }

    void Update()
    {
        bool move = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        SetMotor(move);
    }

    void SetMotor(bool on)
    {
        JointMotor2D motor = new JointMotor2D
        {
            motorSpeed = carMovement.currentMotorSpeed,
            maxMotorTorque = carMovement.maxTorque
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
