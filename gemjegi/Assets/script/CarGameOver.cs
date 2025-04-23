using UnityEngine;
using UnityEngine.SceneManagement;

public class CarGameOver : MonoBehaviour
{
    public float flipAngleThreshold = 75f;
    public float checkDelay = 3f;

    public float stuckSpeedThreshold = 0.1f;

    public float checkInterval = 2f; // 위치 비교 주기
    private float checkTimer = 0f;

    private float flipTimer = 0f;

    private Vector3 lastPosition;
    private Rigidbody rb;

    public float distanceMoved;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    void Update()
    {
        carmovement.LastPlayedStage = SceneManager.GetActiveScene().name;
        CheckFlip();
        CheckStuck();
    }

    void CheckFlip()
    {
        float angle = Vector3.Angle(Vector3.up, transform.up);

        if (angle > flipAngleThreshold)
        {
            flipTimer += Time.deltaTime;
            if (flipTimer > checkDelay)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            flipTimer = 0f;
        }
    }

    void CheckStuck()
    {
        checkTimer += Time.deltaTime;

        bool isMovingKeyPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        if (checkTimer >= checkInterval)
        {
            distanceMoved = Vector3.Distance(lastPosition, transform.position);

            if (isMovingKeyPressed && distanceMoved < stuckSpeedThreshold)
            {
                Debug.Log("게임 오버: 차가 움직이지 않습니다.");
                SceneManager.LoadScene("GameOver");
            }

            lastPosition = transform.position;
            checkTimer = 0f;
        }
    }
}
