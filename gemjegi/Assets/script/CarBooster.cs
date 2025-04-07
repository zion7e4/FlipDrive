using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

public class CarBooster : MonoBehaviour
{
    public carmovement carMovement;

    [SerializeField]
    private float boosterGauge = 20f; //부스터 게이지
    [SerializeField]
    private float maxboosterGauge = 20f; //최대 부스터 게이지
    [SerializeField]
    private float boosterAcc = -8000f; //기본 부스터 가속도

    private float gaugeConsumption = 3f; //초당 부스터 게이지 소모량


    private void Start()
    {
        if (carMovement == null)
        {
            carMovement = GetComponent<carmovement>();
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftShift) && boosterGauge > 0)
        {
            UseBoost();
        }
        else
        {
            carMovement.isBoosting = false;
        }
    }

    private void UseBoost() //부스터 사용
    {
        boosterGauge -= gaugeConsumption * Time.deltaTime;
        boosterGauge = Mathf.Max(boosterGauge, 0f);

        carMovement.isBoosting = true;
    }

    public float CalBoostAmount() //게이지 양에 따른 추가 부스터 가속도 계산
    {
        if (boosterGauge >= 15)
        {
            return boosterAcc * 1.5f;
        }
        else if (boosterGauge >= 10)
        {
            return boosterAcc * 1.2f;
        }
        else if (boosterGauge >= 5)
        {
            return boosterAcc * 1.1f;
        }
        else
        {
            return boosterAcc * 1.0f;
        }
    }
}
