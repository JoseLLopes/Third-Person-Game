using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ThirdPersonGame.PlayerMovement;

namespace ThirdPersonGame.NetworkSystem{
public class NetworkAvatarFollow : MonoBehaviour
{
    [SerializeField] Transform masterPlayer;
    [SerializeField] PhotonView photonView;
    [Header ("Is Mine")]
    [SerializeField] List<GameObject> objectsToDestroy;
    [Header ("Is Not Mine")]
    [SerializeField] List<MonoBehaviour> monoBehaviourToDestroy;

    void Start()
    {

        if(photonView.IsMine){
            //HIDE MESH RENDERER
            masterPlayer = FindObjectOfType<PlayerController>().transform;
            foreach(GameObject obj in objectsToDestroy){
                obj.SetActive(false);
            }
        }
        else{
            //REMOVE BEHAVIOURS
            foreach(MonoBehaviour monoBehaviour in monoBehaviourToDestroy){
                Destroy(monoBehaviour);
            }
        }
    }

    private void Update() {
        if(photonView.IsMine){
            transform.position = masterPlayer.position;
            transform.rotation = masterPlayer.rotation;
        }
    }

    
}
}