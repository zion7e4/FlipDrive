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
    public float rotationForce = 200f; // 회전력
    public float maxRotationSpeed = 300f; // 최대 회전 속도

    [Header("Debug Info")]
    [Tooltip("현재 모터 회전 속도 (음수일수록 빠름)")]
    [SerializeField] private float currentMotorSpeed = 0f;
    [SerializeField] private float currentAngularVelocity = 0f; // 현재 회전 속도

    private Rigidbody2D rb;
    private bool isGrounded = false; // 지면에 닿았는지 여부

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = false; // 회전 가능하도록 설정
    }

    void Update()
    {
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

        ApplyMotor(currentMotorSpeed);

        if(!isGrounded)
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

    // 지면에 닿았는지 여부 확인 (OnCollisionEnter2D 사용)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 지면에 닿았으면 회전 불가능
        }
    }

    // 충돌이 끝났을 때 지면에 떨어진 경우
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 지면에서 떨어지면 회전 가능
        }
    }
}
