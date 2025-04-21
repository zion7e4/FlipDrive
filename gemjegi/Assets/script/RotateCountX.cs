using TMPro;
using UnityEngine;

public class RotateCountX : MonoBehaviour
{
    public TextMeshProUGUI rotateCountX;
    public carmovement carmovement;

    [SerializeField]
    private float groundTimer = 0f;       // 지면 착지 후 타이머

    private void Start()
    {

        // 초기 상태: UI 비활성화
        if (rotateCountX != null)
        {
            rotateCountX.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!carmovement.isOnGround && carmovement.rotatecountforui >= 1)
        {
            rotateCountX.text = "Rotate Count x";
            groundTimer = 0f;
        }
        else
        {
            groundTimer += Time.deltaTime;
            if (groundTimer >= 0.5f)
            {
                rotateCountX.text = " ";
            }
        }
    }
}
