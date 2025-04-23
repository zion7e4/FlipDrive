using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class TMPColorChanger : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI tmpText;

    [Header("Colors")]
    public Color defaultColor = new Color32(139, 69, 19, 255); // 갈색
    public Color clickedColor = Color.black; // 클릭 시 색상

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        tmpText.color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TMPColorChangerManager.Instance.RegisterClick(this);
        tmpText.color = clickedColor;
    }

    public void ResetColor()
    {
        tmpText.color = defaultColor;
    }
}