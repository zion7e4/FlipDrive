using UnityEngine;
using UnityEngine.UI;

public class CarShopManager : MonoBehaviour
{
    [System.Serializable]
    public class Car
    {
        public Sprite carImage;
        public int price;
        public bool isUnlocked;
    }

    public Image carDisplay;
    public Text priceText;
    public Text coinText; // 코인 표시용 텍스트

    public Button buyButton;
    public Button equipButton;
    public Button nextButton;
    public Button prevButton;

    public Car[] cars;

    [Header("현재 보유 코인")]
    public int coins = 0; // 인스펙터에서 조절 가능

    private int currentIndex = 0;
    //PlayerPrefs.DeleteAll();
    private void Start()
    {
        // 첫 번째 차는 항상 언락
        if (PlayerPrefs.GetInt("Car_0", 0) == 0)
            PlayerPrefs.SetInt("Car_0", 1);

        // 첫 번째 차 자동 장착 (처음 실행 시에만)
        if (!PlayerPrefs.HasKey("EquippedCar"))
            PlayerPrefs.SetInt("EquippedCar", 0);

        // 두 번째 차 가격 강제 설정
        if (cars.Length > 1)
            cars[1].price = 50;

        // 데이터 로드
        LoadCarData();
        UpdateUI();
    }



    public void NextCar()
    {
        currentIndex = (currentIndex + 1) % cars.Length;
        UpdateUI();
    }

    public void PrevCar()
    {
        currentIndex = (currentIndex - 1 + cars.Length) % cars.Length;
        UpdateUI();
    }

    public void BuyCar()
    {
        Car currentCar = cars[currentIndex];

        if (coins >= currentCar.price && !currentCar.isUnlocked)
        {
            coins -= currentCar.price;
            currentCar.isUnlocked = true;
            PlayerPrefs.SetInt("Car_" + currentIndex, 1);
            PlayerPrefs.SetInt("Coins", coins);
            UpdateUI();
        }
    }

    public void EquipCar()
    {
        PlayerPrefs.SetInt("EquippedCar", currentIndex);
        UpdateUI();
    }

    private void UpdateUI()
    {
        Car currentCar = cars[currentIndex];
        carDisplay.sprite = currentCar.carImage;

        // 코인 텍스트 표시
        coinText.text = "My coin : " + coins.ToString();

        // 언락 여부 확인
        bool isUnlocked = currentCar.isUnlocked || PlayerPrefs.GetInt("Car_" + currentIndex, 0) == 1;

        priceText.text = currentCar.price.ToString() + " Coins";
        
        // 버튼 표시 조건
        if (isUnlocked)
        {
            buyButton.gameObject.SetActive(false);

            // 현재 장착된 차와 인덱스가 같으면 Equip 버튼 숨기기
            if (PlayerPrefs.GetInt("EquippedCar", -1) == currentIndex)
            {
                equipButton.gameObject.SetActive(false);
            }
            else
            {
                equipButton.gameObject.SetActive(true);
            }
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);
        }

    }

    private void LoadCarData()
    {
        coins = PlayerPrefs.GetInt("Coins", coins);
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].isUnlocked = PlayerPrefs.GetInt("Car_" + i, 0) == 1;
        }
    }
}
