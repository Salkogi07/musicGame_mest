using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn;  // ��ȯ�� ������Ʈ
    public Transform referenceObject; // ������ �Ǵ� ������Ʈ�� Transform
    public Vector2 spawnAreaMin;      // ��ȯ ���� �ּҰ� (���� ������Ʈ�� �߽�����)
    public Vector2 spawnAreaMax;      // ��ȯ ���� �ִ밪 (���� ������Ʈ�� �߽�����)

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
                    // 0.5�� �ڿ� ������Ʈ�� ����
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
