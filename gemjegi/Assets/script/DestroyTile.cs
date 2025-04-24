using UnityEngine;
using System.Collections;

public class DestroyTile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform root = collision.transform.root;

        if (root.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌함!");
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
