using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0; // 현재 코인 수
    public Text coinText;     // UI 텍스트 연결

    private void Start()
    {
        UpdateUI(); // 시작할 때 UI 초기화
    }

    public void AddCoin(int amount = 1)
    {
        coinCount++;
        Debug.Log("코인 추가됨! 현재 개수: " + coinCount);

        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
        else
        {
            Debug.LogWarning("coinText가 연결되지 않았습니다!");
        }
        coinCount += amount;
        UpdateUI();
        Debug.Log("코인 획득! 현재 코인 수: " + coinCount);
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
}