using System.Collections;
using TMPro;
using UnityEngine;

public class TMPColor : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.1f;
    private TextMeshProUGUI TextBoosterOn;

    private void Awake()
    {
        TextBoosterOn = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(ColorLerpLoop));
    }

    private IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));

            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    private IEnumerator ColorLerp(Color start, Color end)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / lerpTime;
            TextBoosterOn.color = Color.Lerp(start, end, percent);

            yield return null;
        }
    }
}
