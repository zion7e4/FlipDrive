using UnityEngine;

public class carmovement : MonoBehaviour
{
    [Header("Character Base Stats (from Design Doc)")]
    [Tooltip("최대 속도 수치")]
    public float MaxSpeed = 5.0f;

    [Tooltip("가속 수치")]
    public float Acceleration = 4.0f;

    private float maxMotorSpeed;
    private float accelerationRate;
    private float decelerationRate;
    private float maxTorque;

    [Header("Wheel Setup")]
    public WheelJoint2D frontWheelJoint;
    public WheelJoint2D backWheelJoint;

    private float currentMotorSpeed = 0f;
    private float holdTime = 0f;

    void Start()
    {
        ApplyDesignStats();
    }

    void Update()
    {
        bool accelerating = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        if (accelerating)
        {
            holdTime += Time.deltaTime;
            float dynamicAccel = accelerationRate * Mathf.Lerp(1f, 2f, holdTime);
            currentMotorSpeed += dynamicAccel * Time.deltaTime;
            currentMotorSpeed = Mathf.Max(currentMotorSpeed, maxMotorSpeed);
        }
        else
        {
            holdTime = 0f;
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

    void ApplyDesignStats()
    {
        maxMotorSpeed = -MaxSpeed * 1000f;
        accelerationRate = -Acceleration * 1000f;
        decelerationRate = Acceleration * 1200f;
        maxTorque = (MaxSpeed + Acceleration) * 800f;
    }

    void OnValidate()
    {
        ApplyDesignStats(); // 인스펙터에서 값 바꾸면 자동 적용
    }
}
