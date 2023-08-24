using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.Gameplay;
using System;

namespace ThirdPersonGame.UI{
    public class ScreensController : MonoBehaviour
    {
        GameplayController gameplayController;
        [SerializeField] List<GameObject> InGameUI;
        [SerializeField] List<GameObject> MenuUi;


        private void Start() {
            gameplayController.OnStartGame += ActiveInGameUI;
            gameplayController.OnEndGame += ActiveMenuUI;
        }

        private void ActiveInGameUI()
        {
            foreach(GameObject obj in InGameUI){
                obj.SetActive(true);
            }
        }

        private void ActiveMenuUI()
        {
            foreach(GameObject obj in MenuUi){
                obj.SetActive(true);
            }
        }
    }
}