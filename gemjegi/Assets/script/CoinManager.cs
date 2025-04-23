using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance; // ½Ì±ÛÅæ ÀÎ½ºÅÏ½º
    public int coinCount = 0;
    //public Text coinText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ¾À ÀüÈ¯ ½Ã ÆÄ±«µÇÁö ¾ÊÀ½
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        coinCount = PlayerPrefs.GetInt("Coins", 0);
        //UpdateUI();
    }

    public void AddCoin(int amount = 1)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("Coins", coinCount);
        PlayerPrefs.Save();
        //UpdateUI();
    }

    public void SpendCoin(int amount)
    {
        coinCount -= amount;
        PlayerPrefs.SetInt("Coins", coinCount);
        PlayerPrefs.Save();
        //UpdateUI();
    }

    /*private void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
    }*/

    public int GetCoins()
    {
        return coinCount;
    }
}
