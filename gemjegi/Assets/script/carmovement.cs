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

    private float boostMultiplier = 1f; // 기본 속도 배율
    private float targetMultiplier = 1f; // 목표 속도 배율
    private float multiplierRestoreSpeed = 0.5f; // 배율 복원 속도 (초당 감소량)

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
            currentMotorSpeed += accelerationRate * Time.deltaTime * boostMultiplier;
            currentMotorSpeed = Mathf.Max(currentMotorSpeed, maxMotorSpeed * boostMultiplier);
        }
        else
        {
            currentMotorSpeed = Mathf.MoveTowards(currentMotorSpeed, 0f, decelerationRate * Time.deltaTime);
        }

        if (!isBoosting && boostMultiplier > 1f)
        {
            // 배율을 천천히 1로 복원
            boostMultiplier = Mathf.MoveTowards(boostMultiplier, targetMultiplier, multiplierRestoreSpeed * Time.deltaTime);
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

    public void ApplyBoostMultiplier(float multiplier)
    {
        boostMultiplier = multiplier;
    }

    public void ResetBoostMultiplier()
    {
        isBoosting = false; // 부스터 종료 상태로 설정
    }
}
