using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic; // HashSet 사용을 위한 추가

public class BreakableTilemapHandler : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 연결
    public TileBase[] breakableTiles; // 여러 개의 부서질 타일 에셋 배열 연결

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 플레이어만 감지
        if (collider.CompareTag("Player"))
        {
            HashSet<Vector3Int> processedTiles = new HashSet<Vector3Int>(); // 이미 처리된 타일을 추적할 HashSet

            // 플레이어의 충돌 지점 계산
            Vector3 playerPosition = collider.transform.position;
            Vector3Int cellPosition = tilemap.WorldToCell(playerPosition); // 플레이어의 월드 좌표를 타일 좌표로 변환

            // 충돌한 타일이 부서질 타일 중 하나인지 확인
            TileBase currentTile = tilemap.GetTile(cellPosition);
            if (currentTile != null && IsBreakableTile(currentTile) && !processedTiles.Contains(cellPosition))
            {
                processedTiles.Add(cellPosition); // 타일이 처리된 목록에 추가
                Debug.Log($"타일 부서짐 시작! 타일 이름: {currentTile.name}");
                StartCoroutine(BreakTileAfterDelay(cellPosition, 1f));
            }
        }
    }

    private bool IsBreakableTile(TileBase tile)
    {
        // 타일이 breakableTiles 배열에 포함되어 있는지 확인
        foreach (TileBase breakableTile in breakableTiles)
        {
            if (tile == breakableTile)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator BreakTileAfterDelay(Vector3Int cellPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        tilemap.SetTile(cellPosition, null); // 타일 제거
        Debug.Log("타일 제거 완료!");
    }
}
