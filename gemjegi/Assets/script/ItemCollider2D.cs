using UnityEngine;

public class ItemCollider2D : MonoBehaviour
{
    public AudioClip collectSound;  // Inspector에서 드래그해서 연결!
    private static AudioSource effectSource;  // 효과음 전용 AudioSource

    private void Start()
    {
        if (effectSource == null)
        {
            // AudioSource가 없다면 추가
            effectSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 코인 충전
            CoinManager coinManager = Object.FindFirstObjectByType<CoinManager>();
            if (coinManager != null)
            {
                coinManager.AddCoin();
            }

            // 효과음이 설정되어 있다면 재생
            if (collectSound != null)
            {
                effectSource.PlayOneShot(collectSound);  // 효과음 재생
            }

            Destroy(gameObject);  // 아이템 파괴
        }
    }
}

