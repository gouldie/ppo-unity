using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour {

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
