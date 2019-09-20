using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviourPunCallbacks
{
    public GameObject mobPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.Instantiate(mobPrefab.name, Vector3.right * 2f, Quaternion.identity);
    }
}
