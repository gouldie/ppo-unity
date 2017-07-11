using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour {

    public byte MaxPlayersPerRoom = 4;
    public GameObject controlPanel;
    public GameObject progressLabel;
    bool isConnecting;

    void Start() {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Connected to master.");

        if (isConnecting) {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnectedFromPhoton() {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);

        Debug.LogWarning("Disconnected from Photon.");
    }

    public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
        Debug.Log("Failed to join random room. Creating..");

		// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom() {
        Debug.Log("Joined room.");

        PhotonNetwork.LoadLevel("starting_area");
    }

    public void connect() {
        isConnecting = true;
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        PhotonNetwork.ConnectUsingSettings("0.1");
    }
}
