using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTile : MonoBehaviour {

	const float encounterRate = 1.0f;
	const float commonRate = 8889/10000f;
	const float uncommonRate = 1000/10000f;
	const float rareRate = 100/10000f;
	const float veryRareRate = 10/10000f;
	const float extremelyRareRate = 1/10000f;

	private GameManager gm;
	public Biomes biome;

	// Use this for initialization
	void Start () {
//		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnTriggerEnter2D(Collider2D col) {
//		if ((Random.Range (0.0f, 10.0f) > encounterRate)) {
//			return;
//		}
//
//		float p = Random.Range (0.0f, 1.0f);
//
//		if (p <= extremelyRareRate) {
//			gm.EnterWildBattle (biome, PokemonRarity.ExtremelyRare);
//		} else if (p <= veryRareRate) {
//			gm.EnterWildBattle (biome, PokemonRarity.VeryRare);
//		} else if (p <= rareRate) {
//			gm.EnterWildBattle (biome, PokemonRarity.Rare);
//		} else if (p <= uncommonRate) {
//			gm.EnterWildBattle (biome, PokemonRarity.Uncommon);
//		} else {
//			gm.EnterWildBattle (biome, PokemonRarity.Common);
//		}
//	}
}

public enum Biomes {
	Grass
}