using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.ScoreSystem{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        [SerializeField] int PlayerScore;
        [SerializeField] List<GameObject> collectablesItemsList;

        private void Awake(){
            if(Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void AddScore(GameObject collectableItem){
            if(collectablesItemsList.Contains(collectableItem)){
                PlayerScore += collectableItem.GetComponent<ICollectable>().collectScore();
                collectableItem.SetActive(false);
            }
        }

        public void AddScoreByIndex(int collectableIndex){
            if(collectablesItemsList[collectableIndex].activeSelf == true){
                PlayerScore += collectablesItemsList[collectableIndex].GetComponent<ICollectable>().collectScore();
                collectablesItemsList[collectableIndex].SetActive(false);
            }
        }

        public int GetCollectableIndex(GameObject collectableItem){
            return collectablesItemsList.IndexOf(collectableItem);
        }

        public void AddCollectableToList(GameObject collectableItem){
            if(!collectablesItemsList.Contains(collectableItem))
                collectablesItemsList.Add(collectableItem);
        }
    }
}

