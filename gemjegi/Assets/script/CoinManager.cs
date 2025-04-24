using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance; // 싱글톤 인스턴스
    public int coinCount = 0;
    public TextMeshProUGUI coinText; // UI 텍스트 연결 (Coins: 0 형태)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        coinCount = PlayerPrefs.GetInt("Coins", 0);
        UpdateUI();
    }

    public void AddCoin(int amount = 1)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("Coins", coinCount);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void SpendCoin(int amount)
    {
        coinCount -= amount;
        PlayerPrefs.SetInt("Coins", coinCount);
        PlayerPrefs.Save();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
    }

    public int GetCoins()
    {
        return coinCount;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        coinText = GameObject.Find("CoinText")?.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }
}
