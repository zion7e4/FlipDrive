using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    public GameObject startButton;
    public Button[] stageButtons;

    private bool isStageSelected = false;
    private string selectedStage;

    private Color defaultColor = new Color(0.59f, 0.29f, 0.0f); // 갈색
    private Color selectedColor = Color.black;

    private Button lastSelectedButton = null;

    private void Start()
    {
        startButton.SetActive(false);

        foreach (Button btn in stageButtons)
        {
            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
            if (btnText != null)
            {
                btnText.color = defaultColor;
            }
        }
    }

    public void SelectStage(string stageName)
    {
        isStageSelected = true;
        selectedStage = stageName;
        startButton.SetActive(true);

        // 현재 클릭한 버튼 찾기
        Button clickedButton = null;
        foreach (Button btn in stageButtons)
        {
            if (btn.name == stageName)
            {
                clickedButton = btn;
                break;
            }
        }

        if (clickedButton == null) return;

        // 이전 버튼 텍스트 색상 되돌리기
        if (lastSelectedButton != null && lastSelectedButton != clickedButton)
        {
            TMP_Text lastText = lastSelectedButton.GetComponentInChildren<TMP_Text>();
            if (lastText != null)
                lastText.color = defaultColor;
        }

        // 현재 버튼 텍스트 색상 검정으로
        TMP_Text clickedText = clickedButton.GetComponentInChildren<TMP_Text>();
        if (clickedText != null)
            clickedText.color = selectedColor;

        // 이번 버튼을 기억해두기
        lastSelectedButton = clickedButton;
        Debug.Log("선택한 스테이지: " + stageName);
    }

    public void StartGame()
    {
        if (isStageSelected)
        {
            SceneManager.LoadScene(selectedStage);
        }
    }
}
