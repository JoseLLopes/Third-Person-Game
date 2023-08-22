using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonGame.ScoreSystem{
    public class Collectable : MonoBehaviour
    {

        [SerializeField] int scoreValue;
        [SerializeField] GameObject collectEffect;
        public int Collect(){
            Instantiate(collectEffect,transform.position,collectEffect.transform.rotation);
            return scoreValue;
        }
    }
}