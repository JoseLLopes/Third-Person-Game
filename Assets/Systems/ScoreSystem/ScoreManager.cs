using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.Gameplay;
using System;
using ThirdPersonGame.Playfab.Data;
using TMPro;


namespace ThirdPersonGame.ScoreSystem{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        [SerializeField] int playerScore;
        [SerializeField] List<GameObject> collectablesItemsList;

        [Header("Interface")]
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text bestscoreText;

        
        private void Awake(){
            if(Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start() {
            GameplayController.Instance.OnEndGame += HandleEndGame;
            PlayfabDataManager.OnGetData += updateBestScoreUI;
            PlayfabDataManager.OnSetData += updateBestScoreUI;
        }

        private void OnDisable() {
            GameplayController.Instance.OnEndGame -= HandleEndGame;
            PlayfabDataManager.OnGetData -= updateBestScoreUI;
            PlayfabDataManager.OnSetData -= updateBestScoreUI;
        }

        private void updateBestScoreUI()
        {
           bestscoreText.text = "Best Score: " + PlayfabDataManager.bestScore;
        }

        private void HandleEndGame()
        {
            foreach(GameObject obj in collectablesItemsList){
                Destroy(obj);
            }
            collectablesItemsList.Clear();
            if(PlayfabDataManager.bestScore == null)
                return;
            if(playerScore > int.Parse(PlayfabDataManager.bestScore))
            {
                PlayfabDataManager.bestScore = ""+playerScore;
                PlayfabDataManager.SetUserData();
            }
            
            



        }

        public void AddScore(GameObject collectableItem){
            if(collectablesItemsList.Contains(collectableItem)){
                playerScore += collectableItem.GetComponent<ICollectable>().collectScore();
                scoreText.text = "Score"+ playerScore;
                collectableItem.SetActive(false);
            }
        }

        public void AddScoreByIndex(int collectableIndex){
            if(collectablesItemsList[collectableIndex].activeSelf == true){
                playerScore += collectablesItemsList[collectableIndex].GetComponent<ICollectable>().collectScore();
                collectablesItemsList[collectableIndex].SetActive(false);
                scoreText.text = "Score"+ playerScore;
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

