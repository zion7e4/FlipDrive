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
    public float rotationForce = 200f; // 회전력
    public float maxRotationSpeed = 300f; // 최대 회전 속도

    [Header("Debug Info")]
    [Tooltip("현재 모터 회전 속도 (음수일수록 빠름)")]
    public float currentMotorSpeed = 0f;
    [SerializeField] 
    private float currentAngularVelocity = 0f; // 현재 회전 속도
    public bool isBoosting = false;

    private Rigidbody2D rb;

    public bool isOnGround;
    public isGroundCheck wheelLeft;
    public isGroundCheck wheelRight;

    private float boostMultiplier = 1f; // 기본 속도 배율
    private float targetMultiplier = 1f; // 목표 속도 배율
    private float multiplierRestoreSpeed = 0.5f; // 배율 복원 속도 (초당 감소량)

    private void Start()
    {
        carBooster = GetComponent<CarBooster>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = false; // 회전 가능하도록 설정
        rb.centerOfMass = new Vector2(0.1f, -0.6f); // 차량의 중심을 아래로 설정
    }

    void Update()
    {
        isOnGround = wheelLeft.isGrounded || wheelRight.isGrounded; // 바퀴가 지면에 닿았는지 확인

        bool accelerating = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool decelerating = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

        if (accelerating)
        {
            currentMotorSpeed += accelerationRate * Time.deltaTime;
            currentMotorSpeed = Mathf.Max(currentMotorSpeed, maxMotorSpeed);
        }
        else if (decelerating)
        {
            currentMotorSpeed -= decelerationRate * Time.deltaTime; // 뒤로 가는 속도 설정
            currentMotorSpeed = Mathf.Max(currentMotorSpeed, maxMotorSpeed);
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

        if (!isOnGround)
        {
            if (accelerating)
            {
                // 회전력 증가
                currentAngularVelocity += rotationForce * Time.deltaTime * (currentMotorSpeed / maxMotorSpeed);

                // 회전 속도 제한 (최대 회전 속도 설정)
                currentAngularVelocity = Mathf.Clamp(currentAngularVelocity, -maxRotationSpeed, maxRotationSpeed);
            }
            else if (decelerating)
            {
                // 반대 방향으로 회전력 증가
                currentAngularVelocity -= rotationForce * Time.deltaTime * (currentMotorSpeed / maxMotorSpeed);

                // 회전 속도 제한 (최대 회전 속도 설정)
                currentAngularVelocity = Mathf.Clamp(currentAngularVelocity, -maxRotationSpeed, maxRotationSpeed);
            }
            else
            {
                // 감속할 때 회전 속도 점차적으로 0으로 감소
                currentAngularVelocity = Mathf.MoveTowards(currentAngularVelocity, 0f, rotationForce * Time.deltaTime);
            }

            // `angularVelocity` 적용하여 차량 회전
            rb.angularVelocity = currentAngularVelocity;
        }
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
