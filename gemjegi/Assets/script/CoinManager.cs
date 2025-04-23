using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0; // 현재 코인 수
    public Text coinText;     // UI 텍스트 연결

    private void Start()
    {
        // 저장된 코인 값을 불러와서 coinCount에 할당
        coinCount = PlayerPrefs.GetInt("Coins", coinCount);
        Debug.Log("초기 코인 수: " + coinCount);
        UpdateUI(); // UI 초기화
    }

    public void AddCoin(int amount = 1)
    {
        coinCount += amount;  // 코인 수 증가
        Debug.Log("코인 추가됨! 현재 개수: " + coinCount);

        // 코인 수 업데이트 후 UI 갱신
        UpdateUI();

        // PlayerPrefs에 코인 수 저장
        PlayerPrefs.SetInt("Coins", coinCount);
        PlayerPrefs.Save(); // 저장 즉시 적용

        Debug.Log("코인 획득! 현재 코인 수: " + coinCount);
    }

    private void Update()
    {
        PlayerPrefs.SetInt("Coins", coinCount);
    }
    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
}
