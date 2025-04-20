using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public GameObject[] carPrefabs; // 인스펙터에 차 프리팹들 순서대로 넣기

    void Awake()
    {
        int selectedCarIndex = PlayerPrefs.GetInt("EquippedCar", 0);

        if (selectedCarIndex >= carPrefabs.Length)
        {
            selectedCarIndex = 0; // 범위 초과 방지
        }

        GameObject car = Instantiate(carPrefabs[selectedCarIndex], transform.position, Quaternion.identity);
    }
}
