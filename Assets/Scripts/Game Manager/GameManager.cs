using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour {

    public GameObject playerPrefab;

    void Start() {
        if (playerPrefab == null) {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
        } else {
            Debug.Log("We are Instantiating LocalPlayer from "+Application.loadedLevelName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
        }
    }

	public void OnLeftRoom() {
		SceneManager.LoadScene(0);
	}

	public void LeaveRoom() {
		PhotonNetwork.LeaveRoom();
	}

    public override void OnPhotonPlayerConnected( PhotonPlayer other  ) {
        Debug.Log( "OnPhotonPlayerConnected() " + other.NickName ); // not seen if you're the player connecting
    }


    public override void OnPhotonPlayerDisconnected( PhotonPlayer other  ) {
        Debug.Log( "OnPhotonPlayerDisconnected() " + other.NickName ); // seen when other disconnects
    }
}
