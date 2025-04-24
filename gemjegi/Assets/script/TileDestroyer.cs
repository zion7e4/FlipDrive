using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileDestroyer : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileBase> breakableTiles;
    public float destroyDelay = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player") || tilemap == null) return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector3Int tilePos = tilemap.WorldToCell(contact.point);
            TileBase tile = tilemap.GetTile(tilePos);

            if (breakableTiles.Contains(tile))
            {
                StartCoroutine(DestroyTileAfterDelay(tilePos));
            }
        }
    }

    private System.Collections.IEnumerator DestroyTileAfterDelay(Vector3Int tilePos)
    {
        yield return new WaitForSeconds(destroyDelay);
        tilemap.SetTile(tilePos, null);
    }
}
