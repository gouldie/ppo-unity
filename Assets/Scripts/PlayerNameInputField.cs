﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Player name input field. Let the user input his name, will appear above the player in the game.
/// </summary>
[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour {

    // Store the PlayerPref Key to avoid typos
    static string playerNamePrefKey = "PlayerName";

    void Start() {
        string defaultName = "";
        InputField _inputField = this.GetComponent<InputField>();

        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }


        PhotonNetwork.playerName = defaultName;
    }


    public void SetPlayerName(string value) {
        // force a trailing space string in case value is an empty string, else playerName would not be updated.
        PhotonNetwork.playerName = value + " ";

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}