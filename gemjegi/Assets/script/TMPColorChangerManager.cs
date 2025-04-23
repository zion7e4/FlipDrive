using UnityEngine;

public class TMPColorChangerManager : MonoBehaviour
{
    public static TMPColorChangerManager Instance;

    private TMPColorChanger lastClicked;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterClick(TMPColorChanger newClicked)
    {
        if (lastClicked != null && lastClicked != newClicked)
        {
            lastClicked.ResetColor();
        }

        lastClicked = newClicked;
    }
}