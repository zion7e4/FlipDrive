using UnityEngine;

public class carmovement : MonoBehaviour
{
    public CarBooster carBooster;

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
    public float currentMotorSpeed = 0f;
    public bool isBoosting = false;

    private void Start()
    {
        if (carBooster == null)
        {
            carBooster = GetComponent<CarBooster>();
        }
    }

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

        if (isBoosting)
        {
            // 부스트 시 최대 속도 제한 해제
            maxTorque = 8000f + -carBooster.CalBoostAmount();
            maxMotorSpeed = -5000f + carBooster.CalBoostAmount();
            currentMotorSpeed += carBooster.CalBoostAmount() * Time.deltaTime;
        }
        else
        {
            // 부스트 종료 후 원래의 최대 속도로 복원
            maxMotorSpeed = -5000f;
            maxTorque = 8000f;
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

