using UnityEngine;
using UnityEngine.SceneManagement;

public class carmovement : MonoBehaviour
{
    [Header("기본 스탯")]
    public float baseMaxSpeed = 5.0f;
    public float baseAcceleration = 4.0f;

    public float motorSpeed;
    public float maxTorque;
    public float currentMotorSpeed = 0f;
    public float motorLerpSpeed = 5f;

    public WheelJoint2D frontWheelJoint;
    public WheelJoint2D backWheelJoint;
    public Rigidbody2D carBody;

    [Header("회전 및 부스터")]
    public float rotationForce = 30f;
    public float maxRotationSpeed = 300f;
    public float multiplierRestoreSpeed = 0.5f;

    private float currentAngularVelocity = 0f; // 현재 회전 속도

    public bool isOnGround;
    public isGroundCheck wheelLeft;
    public isGroundCheck wheelRight;

    public float boostMultiplier = 1f;
    public float targetMultiplier = 1f;
    public bool isBoosting = false;

    private float rotateangle = 0f;
    private int rotatecount = 0;
    public int rotatecountforui = 0;
    public CarBooster carBooster;

    void Start()
    {
        ApplyDesignStats();
        AdjustCenterOfMass();
        AdjustSuspension();
    }

    void ApplyDesignStats()
    {
        motorSpeed = -baseMaxSpeed * 800f; // 반드시 음수!
        maxTorque = (baseMaxSpeed + baseAcceleration) * 400f;
        currentMotorSpeed = 0f;
    }

    void AdjustCenterOfMass()
    {
        if (carBody != null)
        {
            carBody.centerOfMass = new Vector2(0.2f, -0.7f); // 붕 뜨는 거 방지
        }
    }

    void AdjustSuspension()
    {
        JointSuspension2D suspension = frontWheelJoint.suspension;
        suspension.dampingRatio = 3.0f;
        suspension.frequency = 8.0f;

        frontWheelJoint.suspension = suspension;
        backWheelJoint.suspension = suspension;
    }

    void FixedUpdate()
    {
        isOnGround = wheelLeft.isGrounded || wheelRight.isGrounded;
        bool isAirborne = !wheelLeft.isGrounded && !wheelRight.isGrounded;

        bool accelerating = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool decelerating = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

        float targetSpeed = 0f;

        if (accelerating)
        {
            targetSpeed = motorSpeed * boostMultiplier; // 음수면 앞으로 감
        }
        /*else if (decelerating)
        {
            targetSpeed = -motorSpeed; // 뒤로 가기
        }*/

        currentMotorSpeed = Mathf.Lerp(currentMotorSpeed, targetSpeed, Time.fixedDeltaTime * motorLerpSpeed);

        if (!isBoosting && boostMultiplier > 1f)
            boostMultiplier = Mathf.MoveTowards(boostMultiplier, targetMultiplier, multiplierRestoreSpeed * Time.fixedDeltaTime);

        SetMotor(accelerating || decelerating);

        // 공중 회전
        if (!isOnGround)
        {
            float direction = 0f;
            if (accelerating)
            {
                direction = 1f;
            }
            else if (decelerating)
            {
                direction = -1f;
            }

            if (direction != 0f)
            {
                float angularBoost = rotationForce * direction * Time.fixedDeltaTime;
                carBody.angularVelocity = Mathf.Clamp(carBody.angularVelocity + angularBoost, -maxRotationSpeed, maxRotationSpeed);
                carBody.AddTorque(rotationForce * direction, ForceMode2D.Force);
            }
        }
        /*else
        {
            // 회전 감속
            carBody.angularVelocity = Mathf.MoveTowards(carBody.angularVelocity, 0f, rotationForce * Time.fixedDeltaTime);
        }*/

        RotateCount();

        if (isOnGround && rotatecount > 0)
        {
            carBooster.BoosterGauge += (rotatecount * 5f);
            rotatecount = 0;
            rotateangle = 0f;
        }
    }

    void SetMotor(bool on)
    {
        JointMotor2D motor = new JointMotor2D
        {
            motorSpeed = currentMotorSpeed,
            maxMotorTorque = maxTorque
        };

        frontWheelJoint.useMotor = false;
        backWheelJoint.useMotor = on;

        if (on)
        {
            backWheelJoint.motor = motor;
        }
    }

    void RotateCount()
    {
        rotateangle += Mathf.Abs(carBody.angularVelocity * Time.fixedDeltaTime);

        if (rotateangle >= 360f)
        {
            rotatecount += 1;
            rotatecountforui += 1;
            rotateangle -= 360f;
        }
    }

    public void ApplyBoostMultiplier(float multiplier)
    {
        boostMultiplier = multiplier;
        isBoosting = true;
    }

    public void ResetBoostMultiplier()
    {
        isBoosting = false;
    }

    void OnValidate()
    {
        ApplyDesignStats();
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