using UnityEngine;

public class CarMovement_Motor : MonoBehaviour
{
    [Header("Character Base Stats (from Design Doc)")]
    public float baseMaxSpeed = 5.0f;       // 예: 3 ~ 6
    public float baseAcceleration = 4.0f;   // 예: 3 ~ 6

    private float motorSpeed;
    private float maxTorque;

    public WheelJoint2D frontWheelJoint;
    public WheelJoint2D backWheelJoint;

    void Start()
    {
        ApplyDesignStats();
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

    void ApplyDesignStats()
    {
        motorSpeed = -baseMaxSpeed * 1000f;
        maxTorque = (baseMaxSpeed + baseAcceleration) * 800f;
    }

    void OnValidate()
    {
        ApplyDesignStats(); // 인스펙터 수정 시 자동 반영
    }
}
