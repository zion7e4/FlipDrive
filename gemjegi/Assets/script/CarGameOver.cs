using UnityEngine;
using UnityEngine.SceneManagement;

public class CarGameOver : MonoBehaviour
{
    public float flipAngleThreshold = 75f; // 차가 얼마나 기울어졌을 때 게임오버로 볼지
    public float checkDelay = 2f; // 일정 시간 뒤집힌 상태가 지속될 경우에만 게임오버
    private float flipTimer = 0f;

    void Update()
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
            flipTimer = 0f; // 다시 정상자세로 돌아오면 타이머 리셋
        }

    }
}
