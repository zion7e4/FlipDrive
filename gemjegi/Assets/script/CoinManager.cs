using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText; // UI 텍스트 연결 (Coins: 0 형태)
    private int coinCount = 0; // 현재 코인 개수

    // 코인 추가 함수 (외부에서 호출)
    public void AddCoin()
    {
        coinCount++;
        coinText.text = "Coins: " + coinCount;
    }
}