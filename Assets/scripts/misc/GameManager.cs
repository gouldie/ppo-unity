using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject battle;
	public Camera mainCamera;
	public Area area;

	public Transform friendlyPodium;
	public Transform enemyPodium;
	public GameObject emptyPokemon;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterWildBattle(Biomes biome, PokemonRarity rarity) {
		Pokemon pokemon = area.GetWildGrassPokemon (rarity);

		GameObject friendlyPoke = Instantiate (emptyPokemon, friendlyPodium.transform.position, Quaternion.identity) as GameObject;
		GameObject enemyPoke = Instantiate (emptyPokemon, enemyPodium.transform.position, Quaternion.identity) as GameObject;

		friendlyPoke.transform.parent = friendlyPodium;
		enemyPoke.transform.parent = enemyPodium;

		enemyPoke.GetComponent<SpriteRenderer> ().sprite = pokemon.image;

		Pokemon tempPoke = enemyPoke.AddComponent<Pokemon> () as Pokemon;
		tempPoke.ImportFromPrefab (pokemon);

		battle.GetComponent<Battle> ().WildBattle (mainCamera);
		//player.GetComponent<PlayerMovement> ().SetMove (false);
	}

	public void LeaveBattle() {
		
	}
}
