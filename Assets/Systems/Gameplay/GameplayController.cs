using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


namespace ThirdPersonGame.Gameplay{
public class GameplayController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform[] playerSpawnPoints;

    public virtual void StartGame(){
        Transform randomSpawnPoint = playerSpawnPoints[Random.Range(0,playerSpawnPoints.Length)];
        Instantiate(playerPrefab,randomSpawnPoint.position, playerPrefab.transform.rotation);
    }

}
}