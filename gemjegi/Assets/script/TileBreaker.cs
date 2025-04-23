using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class TileBreaker : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileBase> breakableTiles;

    private HashSet<Vector3Int> breakingTiles = new HashSet<Vector3Int>();

    private void Update()
    {
        if (tilemap == null)
        {
            Debug.LogError(" Tilemap이 연결되지 않았습니다!");
            return;
        }

        Vector3 playerWorldPos = transform.position;
        Vector3Int cellPos = tilemap.WorldToCell(playerWorldPos);
        TileBase currentTile = tilemap.GetTile(cellPos);

        if (currentTile != null)
        {
            Debug.Log("현재 밟은 타일: " + currentTile.name);
        }

        if (currentTile != null && breakableTiles.Contains(currentTile) && !breakingTiles.Contains(cellPos))
        {
            Debug.Log("타일 삭제 예정: " + cellPos);
            breakingTiles.Add(cellPos);
            StartCoroutine(BreakTileAfterDelay(cellPos, 1f));
        }
    }

    private IEnumerator BreakTileAfterDelay(Vector3Int cellPos, float delay)
    {
        yield return new WaitForSeconds(delay);
        tilemap.SetTile(cellPos, null);
        Debug.Log(" 타일 삭제됨: " + cellPos);
        breakingTiles.Remove(cellPos);
    }
}
