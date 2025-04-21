using UnityEngine;
using UnityEngine.UI;

public class GaugeUI : MonoBehaviour
{
    public Image gauge;
    [SerializeField]
    private CarBooster carbooster;


    private void Update()
    {
        SetGauge();
    }

    private void SetGauge()
    {
        float targetFillAmount = carbooster.BoosterGauge / carbooster.maxboosterGauge;
        gauge.fillAmount = Mathf.Lerp(gauge.fillAmount, targetFillAmount, Time.deltaTime * 5f);
    }
}
