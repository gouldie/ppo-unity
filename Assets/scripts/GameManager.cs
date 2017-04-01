using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject battleScreen;
	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterBattle(PokemonRarity rarity) {
		// Get the main camera width & height
		float camHeight = 2f * mainCamera.orthographicSize;
		float camWidth = camHeight * mainCamera.aspect;

		// Set the battle screen size to half of the main camera width & height
		battleScreen.GetComponent<RectTransform> ().sizeDelta = new Vector2 (camWidth/2, camHeight/2);

		// Disable player movement and show battle screen
		battleScreen.GetComponent<Canvas> ().enabled = true;
		player.GetComponent<PlayerMovement> ().enabled = false;
	}

	public void LeaveBattle() {
		
	}
}
