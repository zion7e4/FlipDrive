using UnityEngine;

public class ItemCollider2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 게 Player 태그일 경우
        if (collision.CompareTag("Player"))
        {
            // CoinManager 찾아서 코인 1개 추가
            FindObjectOfType<CoinManager>().AddCoin();
            Destroy(gameObject); // 코인 오브젝트 제거
        }
    }
}