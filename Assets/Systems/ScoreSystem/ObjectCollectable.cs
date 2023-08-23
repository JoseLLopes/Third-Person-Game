using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonGame.ScoreSystem{
    public class ObjectCollectable : MonoBehaviour, ICollectable
    {

        [SerializeField] int scoreValue;
        [SerializeField] GameObject collectEffect;
        public int collectScore(){
            if(collectEffect)
                Instantiate(collectEffect,transform.position,collectEffect.transform.rotation);
            return scoreValue;
        }
    }
}