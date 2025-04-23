using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    private static BGMPlayer instance;
    private AudioSource audioSource;  // BGM을 재생할 AudioSource

    public AudioClip bgmClip;  // Inspector에서 BGM Clip을 연결

    private void Awake()
    {
        // 이미 존재하는 BGMPlayer가 있다면 새로 만든 건 파괴
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // 이 오브젝트는 씬 전환 시 파괴되지 않음
        instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource 초기화 및 BGM 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;  // BGM을 반복 재생
        audioSource.Play();  // BGM 재생 시작
    }
}
