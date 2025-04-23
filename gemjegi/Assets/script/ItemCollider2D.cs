using UnityEngine;

public class ItemCollider2D : MonoBehaviour
{
<<<<<<< HEAD
    public AudioClip collectSound; // Inspector에서 연결할 효과음
=======
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
>>>>>>> heeyoung-1-2-1

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
<<<<<<< HEAD
            // 코인 매니저에 코인 추가
=======
            // 코인 충전
>>>>>>> heeyoung-1-2-1
            CoinManager coinManager = Object.FindFirstObjectByType<CoinManager>();
            if (coinManager != null)
            {
                coinManager.AddCoin();
            }
<<<<<<< HEAD
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
=======

            // 효과음이 설정되어 있다면 재생
            if (collectSound != null)
            {
                effectSource.PlayOneShot(collectSound);  // 효과음 재생
            }

            Destroy(gameObject);  // 아이템 파괴
        }
    }
}

>>>>>>> heeyoung-1-2-1
