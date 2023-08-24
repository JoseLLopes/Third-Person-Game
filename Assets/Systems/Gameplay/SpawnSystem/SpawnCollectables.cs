using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.ScoreSystem;


namespace ThirdPersonGame.Gameplay.SpawnSystem{
    public class SpawnCollectables : MonoBehaviour
    {
        [Range(3,50)]
        [SerializeField] float timeTospawn = 10;
        [SerializeField] GameObject objectCollectable;
        [SerializeField] ScoreSpawnPointArea[] ScoreSpawnPointAreas;
        
       
        
        private void Start() {
           GameplayController.Instance.OnStartGame += StartSpawn;
           GameplayController.Instance.OnEndGame += StopSpawn;
        }

        private void OnDisable() {
            GameplayController.Instance.OnStartGame -= StartSpawn;
            GameplayController.Instance.OnEndGame -= StopSpawn;
        }

        void StartSpawn(){
            if(GameplayController.Instance.currentGameMode == GameplayController.GAMEMODE.SINGLE){
                StartCoroutine(SpawnCollectable());
            }
        }
        
        IEnumerator SpawnCollectable(){
            var collectableObject = Instantiate(objectCollectable,
                                                GetRandomPosition(),
                                                objectCollectable.transform.rotation);
            ScoreManager.Instance.AddCollectableToList(collectableObject);
            yield return new WaitForSeconds(timeTospawn);
            StartCoroutine(SpawnCollectable());

        }

        Vector3 GetRandomPosition(){
            int randomSpawnPointIndex = Random.Range(0,ScoreSpawnPointAreas.Length);
            return ScoreSpawnPointAreas[randomSpawnPointIndex].GetRandomPosition();
        }

        void StopSpawn(){
            StopAllCoroutines();
        }
    }
}