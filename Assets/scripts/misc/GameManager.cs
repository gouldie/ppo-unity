using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject battle;
	public Camera mainCamera;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterWildBattle(Biomes biome, PokemonRarity rarity) {
		Debug.Log ("Encounter" + biome + rarity);

		battle.GetComponent<Battle> ().WildBattle (mainCamera);
		player.GetComponent<PlayerMovement> ().SetMove (false);
	}

	public void LeaveBattle() {
		
	}
}
