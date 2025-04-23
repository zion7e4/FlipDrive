using UnityEngine;

public class CarSpriteLoader : MonoBehaviour
{
    public SpriteRenderer carSpriteRenderer;
    public Sprite[] carSprites; // ÀÎµ¦½º = Car ¹è¿­ ÀÎµ¦½º

    void Start()
    {
        int selectedCar = PlayerPrefs.GetInt("EquippedCar", 0);

        if (selectedCar >= 0 && selectedCar < carSprites.Length)
        {
            carSpriteRenderer.sprite = carSprites[selectedCar];
        }
        else
        {
            Debug.LogWarning("¼±ÅÃµÈ Â÷ ÀÎµ¦½º°¡ ¹üÀ§¸¦ ¹þ¾î³µ½À´Ï´Ù.");
        }
    }
}
