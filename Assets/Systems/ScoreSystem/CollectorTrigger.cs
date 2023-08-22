using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.ScoreSystem{
    public class CollectorTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if(other.transform.TryGetComponent<Collectable>(out Collectable collectable)){
                ScoreManager.AddScore((collectable.Collect()));
                Destroy(other.gameObject);
            }
        }
    }
}