using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.ScoreSystem{
    public class CollectorTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if(other.transform.TryGetComponent<ICollectable>(out ICollectable collectable)){
                ScoreManager.Instance.AddScore(other.gameObject);
            }
        }
    }
}