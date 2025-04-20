using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public GameObject StartButton; // "시작하기" 버튼
    public static string SelectedStage;
    public static bool IsStageSelected = false;

    private void Start()
    {
        StartButton.SetActive(false);
    }
    // Stage 버튼 클릭 시 호출
    public void SelectStage(string stageName)
    {
        IsStageSelected = true;
        StartButton.SetActive(true); // 시작하기 버튼 표시

        // 필요하면 선택된 스테이지 이름 저장
        SelectedStage = stageName;
    }

    /*// 시작하기 버튼 클릭 시 호출
    public void StartGame()
    {
        if (IsStageSelected)
        {
            SceneManager.LoadScene(SelectedStage);
        }
    }*/

}
