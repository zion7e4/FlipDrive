using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDestroyer : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 참조
    public float destroyDelay = 1f; // 타일 파괴까지 대기 시간

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3Int tilePos = tilemap.WorldToCell(contactPoint);
            TileBase tile = tilemap.GetTile(tilePos);

            if (tile != null && tile.name == "BreakableTile") // 혹은 특정 타일 종류 비교
            {
                StartCoroutine(DestroyTileAfterDelay(tilePos));
            }
        }
    }

    private System.Collections.IEnumerator DestroyTileAfterDelay(Vector3Int tilePos)
    {
        yield return new WaitForSeconds(destroyDelay);
        tilemap.SetTile(tilePos, null); // 해당 타일 삭제
    }
}
