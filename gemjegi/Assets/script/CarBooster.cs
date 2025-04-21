using UnityEngine;
using TMPro;
public class CarBooster : MonoBehaviour
{
    public carmovement carMovement;

    [SerializeField]
    private float boosterGauge;
    public float BoosterGauge
    { 
        set => boosterGauge = Mathf.Clamp(value, 0f, maxboosterGauge); // 부스터 게이지가 최대 부스터 게이지를 초과하지 않도록 설정
        get => boosterGauge;
    }// 현재 부스터 게이지

    
    public float maxboosterGauge = 0f; // 최대 부스터 게이지
    private float gaugeConsumption; // 게이지 소모량
    private float speedMultiplier; // 속도 배율
    private float boostDuration; // 부스터 지속 시간

    private bool isBoostActive = false; // 부스터 활성화 상태
    private float boostEndTime; // 부스터 종료 시간 (Time.time)
    public GameObject TextBoosterOn;
    public TextMeshProUGUI TextMeshProUGUI;

    private void Awake()
    {
        carMovement = GetComponent<carmovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && BoosterGauge > 0 && !isBoostActive)
        {
            // 부스터 등급 결정
            DetermineBoostTier();
        }

        if (isBoostActive)
        {
            if (Time.time >= boostEndTime)
            {
                // 부스터 종료
                EndBoost();
                TextBoosterOn.SetActive(false);
            }

        }
        else
        {
            carMovement.isBoosting = false;
        }
    }

    private void DetermineBoostTier()
    {
        float gaugePercent = (BoosterGauge / maxboosterGauge) * 100f;

        if (gaugePercent >= 100f)
        {
            // 특대형
            ActivateBoost(100f, 1.8f, 2.5f);
        }
        else if (gaugePercent >= 75f)
        {
            // 대형
            ActivateBoost(75f, 1.6f, 2f);
        }
        else if (gaugePercent >= 50f)
        {
            // 중형
            ActivateBoost(50f, 1.4f, 1.5f);
        }
        else if (gaugePercent >= 25f)
        {
            // 소형
            ActivateBoost(25f, 1.2f, 1f);
        }
    }

    private void ActivateBoost(float consumptionPercent, float multiplier, float duration)
    {
        gaugeConsumption = (consumptionPercent / 100f) * maxboosterGauge;
        speedMultiplier = multiplier;
        boostDuration = duration;

        if (BoosterGauge >= gaugeConsumption)
        {
            // 게이지를 소모하고 부스터 활성화
            BoosterGauge -= gaugeConsumption;
            isBoostActive = true;
            boostEndTime = Time.time + boostDuration;
            carMovement.isBoosting = true;

            // 속도 배율 적용
            carMovement.ApplyBoostMultiplier(speedMultiplier);
        }
        if (duration == 2.5f)
        {
            TextMeshProUGUI.text = "XL BOOSTER ON!";
            TextBoosterOn.SetActive(true);
        }
        else if (duration == 2f)
        {
            TextMeshProUGUI.text = "L BOOSTER ON!";
            TextBoosterOn.SetActive(true);
        }
        else if (duration == 1.5f)
        {
            TextMeshProUGUI.text = "M BOOSTER ON!";
            TextBoosterOn.SetActive(true);
        }
        else if (duration == 1f)
        {
            TextMeshProUGUI.text = "S BOOSTER ON!";
            TextBoosterOn.SetActive(true);
        }
    }

    private void EndBoost()
    {
        isBoostActive = false;
        carMovement.isBoosting = false;

        // 속도 배율 해제
        carMovement.ResetBoostMultiplier();
    }

    public float GetCurrentGaugePercent()
    {
        return (BoosterGauge / maxboosterGauge) * 100f;
    }
}
