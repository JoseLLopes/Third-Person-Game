using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.ScoreSystem{
    public class ScoreManager : MonoBehaviour
    {
        static int PlayerScore;

        public static void AddScore(int amount){
            PlayerScore += amount;
        }
    }
}