using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

public class CarBooster : MonoBehaviour
{
    public carmovement carMovement;

    [SerializeField]
    private float boosterGauge = 20f; //�ν��� ������
    [SerializeField]
    private float maxboosterGauge = 20f; //�ִ� �ν��� ������
    [SerializeField]
    private float boosterAcc = -8000f; //�⺻ �ν��� ���ӵ�

    private float gaugeConsumption = 3f; //�ʴ� �ν��� ������ �Ҹ�


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

    private void UseBoost() //�ν��� ���
    {
        boosterGauge -= gaugeConsumption * Time.deltaTime;
        boosterGauge = Mathf.Max(boosterGauge, 0f);

        carMovement.isBoosting = true;
    }

    public float CalBoostAmount() //������ �翡 ���� �߰� �ν��� ���ӵ� ���
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
