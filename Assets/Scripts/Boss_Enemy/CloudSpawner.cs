using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn;  // 소환할 오브젝트
    public Transform referenceObject; // 기준이 되는 오브젝트의 Transform
    public Vector2 spawnAreaMin;      // 소환 범위 최소값 (기준 오브젝트를 중심으로)
    public Vector2 spawnAreaMax;      // 소환 범위 최대값 (기준 오브젝트를 중심으로)

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (GameManager.Instance.IsCloud())
            {
                int spawnCount = Random.Range(2, 6);
                for (int i = 0; i < spawnCount; i++)
                {
                    Vector2 spawnPosition = new Vector2(
                        Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                        Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                    );
                    Vector3 finalSpawnPosition = referenceObject.position + (Vector3)spawnPosition;
                    GameObject spawnedObject = Instantiate(objectToSpawn, finalSpawnPosition, Quaternion.identity);
                    // 0.5초 뒤에 오브젝트를 삭제
                    Destroy(spawnedObject, 0.5f);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDrawGizmos()
    {
        if (referenceObject == null) return;

        Gizmos.color = Color.red;
        Vector3 bottomLeft = referenceObject.position + (Vector3)spawnAreaMin;
        Vector3 bottomRight = referenceObject.position + new Vector3(spawnAreaMax.x, spawnAreaMin.y);
        Vector3 topLeft = referenceObject.position + new Vector3(spawnAreaMin.x, spawnAreaMax.y);
        Vector3 topRight = referenceObject.position + (Vector3)spawnAreaMax;

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(topRight, topLeft);
    }
}
