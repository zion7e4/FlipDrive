using UnityEngine;
using TMPro;
using System.Collections;

public class RotateCountUI : MonoBehaviour
{
    public TextMeshProUGUI rotateCountUI;
    public carmovement carmovement;

    [SerializeField]
    private float groundTimer = 0f;       // 지면 착지 후 타이머

    private void Start()
    {

        // 초기 상태: UI 비활성화
        if (rotateCountUI != null)
        {
            rotateCountUI.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(!carmovement.isOnGround && carmovement.rotatecountforui >= 1)
        {
            rotateCountUI.text = "x" + carmovement.rotatecountforui.ToString();
            groundTimer = 0f;
        }
        else
        {
            groundTimer += Time.deltaTime;
            if (groundTimer >= 0.5f)
            {
                rotateCountUI.text = " ";
                carmovement.rotatecountforui = 0;
            }
        }
    }
}