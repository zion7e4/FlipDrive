using UnityEngine;

public class ItemCollider2D : MonoBehaviour
{
    public AudioClip collectSound; // Inspector에서 연결할 효과음

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 코인 매니저에 코인 추가
            CoinManager coinManager = Object.FindFirstObjectByType<CoinManager>();
            if (coinManager != null)
            {
                coinManager.AddCoin();
            }
            else
            {
                Debug.LogWarning("CoinManager를 찾을 수 없습니다!");
            }

            // 효과음 재생 (AudioSource 없어도 됨!)
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // 코인 파괴
            Destroy(gameObject);
        }
    }
}