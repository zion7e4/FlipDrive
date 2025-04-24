using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public float rotationForce = 200f; // 회전력
    public float maxRotationSpeed = 300f; // 최대 회전 속도

    [Header("Boost Settings")]
    public float multiplierRestoreSpeed = 0.5f; // 배율 복원 속도 (초당 감소량)

    [Header("Debug Info")]
    [Tooltip("현재 모터 회전 속도 (음수일수록 빠름)")]
    public float currentMotorSpeed = 0f;
    public float currentAngularVelocity = 0f; // 현재 회전 속도
    public bool isBoosting = false;

    private Rigidbody2D rb;
    public bool isOnGround;
    public isGroundCheck wheelLeft;
    public isGroundCheck wheelRight;

    private float boostMultiplier = 1f; // 기본 속도 배율
    private float targetMultiplier = 1f; // 목표 속도 배율

    public int rotatecount = 0; // 회전수
    public int rotatecountforui = 0;
    public float rotateangle = 0f; // 회전 각도
    public float currentRotateAngle;

    public CarBooster carBooster;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = false; // 회전 가능하도록 설정
        rb.centerOfMass = new Vector2(0.1f, -0.6f); // 차량의 중심을 아래로 설정

        if (carBooster == null)
        {
            carBooster = GetComponent<CarBooster>();
        }
    }

    void Update()
    {
        isOnGround = wheelLeft.isGrounded || wheelRight.isGrounded; // 바퀴가 지면에 닿았는지 확인

        bool accelerating = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool decelerating = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

        if (accelerating)
        {
            currentMotorSpeed += accelerationRate * Time.deltaTime * boostMultiplier;
            currentMotorSpeed = Mathf.Max(currentMotorSpeed, maxMotorSpeed * boostMultiplier);
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

            // angularVelocity 적용하여 차량 회전
            rb.angularVelocity = currentAngularVelocity;

        }

        rotateCount();

        if (isOnGround && rotatecount > 0) // 부스터 게이지 추가
        {
            carBooster.BoosterGauge += (rotatecount * 5f);

            rotatecount = 0;
            rotateangle = 0f;
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

    public void rotateCount()
    {
        currentRotateAngle = rb.rotation; // 현재 회전각도
        rotateangle += Mathf.Abs(rb.angularVelocity * Time.deltaTime);

        if (rotateangle >= 360f) // 회전각도가 360도 이상일 때
        {
            rotatecount += 1; // 회전수 + 1
            rotatecountforui += 1;
            rotateangle -= 360f; // 회전각도 - 360도
        }
    }

    public void ApplyBoostMultiplier(float multiplier)
    {
        boostMultiplier = multiplier;
        isBoosting = true;
    }

    public void ResetBoostMultiplier()
    {
        isBoosting = false; // 부스터 종료 상태로 설정
    }

    public static string LastPlayedStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            LastPlayedStage = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene("GameOver");
        }

        if (collision.CompareTag("Finish"))
        {
            LastPlayedStage = SceneManager.GetActiveScene().name;

            if (LastPlayedStage == "Stage1" || LastPlayedStage == "Stage2")
            {
                SceneManager.LoadScene("GameClear");
            }
            else if (LastPlayedStage == "Stage3")
            {
                SceneManager.LoadScene("GameFinish");
            }
        }
    }
}