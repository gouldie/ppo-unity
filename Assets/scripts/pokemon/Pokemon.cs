using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour {

	public Sprite image;
	public PokemonName name;
	public PokemonType type;
	public PokemonRarity rarity;
	public int[] baseStats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum PokemonRarity {
	Common,
	Uncommon,
	Rare,
	VeryRare,
	ExtremelyRare
}

public enum PokemonName {
	Bulbasaur,
	Pidgey,
	Rattata,
	Squirtle,
	Charmander
}

public enum PokemonType {
	Grass,
	Normal,
	Flying
}