using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawnPointArea : MonoBehaviour
{
    [SerializeField] Vector3 spawnArea;

    public Vector3 GetRandomPosition(){
        float randomX = Random.Range(-spawnArea.x/2,spawnArea.x/2);
        float randomY = Random.Range(-spawnArea.y/2,spawnArea.y/2);
        float randomZ = Random.Range(-spawnArea.z/2,spawnArea.z/2);
        
        Vector3 randomPosition = transform.position + new Vector3(randomX,randomY,randomZ);
        return randomPosition;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position,spawnArea);
    }
}
