using UnityEngine;

public class carmovement : MonoBehaviour
{
    [Header("Wheel Joint Settings")]
    public WheelJoint2D frontWheelJoint;
    public WheelJoint2D backWheelJoint;

    [Header("Speed Settings")]
    [Tooltip("모터최대속도")]
    public float maxMotorSpeed = -5000f;
    public float accelerationRate = -5000f;
    public float decelerationRate = 1000f;
    public float maxTorque = 8000f;

    [Header("Debug Info")]
    [Tooltip("현재 모터 회전 속도 (음수일수록 빠름)")]
    [SerializeField] private float currentMotorSpeed = 0f;

    void Update()
    {
        bool accelerating = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        if (accelerating)
        {
            currentMotorSpeed += accelerationRate * Time.deltaTime;
            currentMotorSpeed = Mathf.Max(currentMotorSpeed, maxMotorSpeed);
        }
        else
        {
            currentMotorSpeed = Mathf.MoveTowards(currentMotorSpeed, 0f, decelerationRate * Time.deltaTime);
        }

        ApplyMotor(currentMotorSpeed);
    }

    void ApplyMotor(float speed)
    {
        JointMotor2D motor = new JointMotor2D
        {
            motorSpeed = speed,
            maxMotorTorque = maxTorque
        };

        frontWheelJoint.motor = motor;
        backWheelJoint.motor = motor;

        frontWheelJoint.useMotor = true;
        backWheelJoint.useMotor = true;
    }
}
