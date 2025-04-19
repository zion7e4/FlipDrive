using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public GameObject startButton; // "시작하기" 버튼
    private bool isStageSelected = false;

    private void Start()
    {
        startButton.SetActive(false);
    }
    // Stage 버튼 클릭 시 호출
    public void SelectStage(string stageName)
    {
        isStageSelected = true;
        startButton.SetActive(true); // 시작하기 버튼 표시

        // 필요하면 선택된 스테이지 이름 저장
        selectedStage = stageName;
    }

    // 시작하기 버튼 클릭 시 호출
    public void StartGame()
    {
        if (isStageSelected)
        {
            SceneManager.LoadScene(selectedStage);
        }
    }

    private string selectedStage;
}
