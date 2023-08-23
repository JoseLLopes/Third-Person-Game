using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.ScoreSystem;
using Photon.Pun;

public class NetworkCollectorTrigger : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    private void OnTriggerEnter(Collider other) {
        if(photonView.IsMine){
            if(other.transform.TryGetComponent<ICollectable>(out ICollectable collectable)){
                int scoreCollected = ScoreManager.Instance.GetCollectableIndex(other.gameObject);
                photonView.RPC("RPC_AddScore",RpcTarget.AllBuffered, scoreCollected);                
            }
        }
    }

    [PunRPC]
    void RPC_AddScore(int scoreIndex){
        ScoreManager.Instance.AddScoreByIndex(scoreIndex);
    }
}
