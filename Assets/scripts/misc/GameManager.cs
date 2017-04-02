using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject battle;
	public Camera mainCamera;
	public Area area;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterWildBattle(Biomes biome, PokemonRarity rarity) {
		Debug.Log ("Encounter" + biome + rarity);

		PokemonName pokemonName = area.GetWildGrassPokemon (rarity);

		battle.GetComponent<Battle> ().WildBattle (mainCamera);
		player.GetComponent<PlayerMovement> ().SetMove (false);
	}

	public void LeaveBattle() {
		
	}
}
