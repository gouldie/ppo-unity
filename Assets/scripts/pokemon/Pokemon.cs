using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour {

	public Sprite image;
	public PokemonName name;
	public PokemonType[] type;
	public PokemonEvolution evolution;
	public BaseStats baseStats;

	// Use this for initialization
	void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void ImportFromPrefab(Pokemon pokemon) {
		this.image = pokemon.image;
		this.name = pokemon.name;
		this.type = pokemon.type;
		this.evolution = pokemon.evolution;
		this.baseStats = pokemon.baseStats;
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
	Ivysaur,
	Venusaur,
	Squirtle,
	Wartortle,
	Blastoise,
	Charmander,
	Charmeleon,
	Charizard,
	Pidgey,
	Pidgeotto,
	Pidgeot,
	Pikachu,
	Raichu
}

public enum PokemonType {
	Normal,
	Fire,
	Fighting,
	Water,
	Flying,
	Grass,
	Poison,
	Electric,
	Ground,
	Psychic,
	Rock,
	Ice,
	Bug,
	Dragon,
	Ghost,
	Dark,
	Steel,
	Fairy
}

[System.Serializable]
public class PokemonEvolution {
	public GameObject evolveTo;
	public int evolveLevel;
}

[System.Serializable]
public class BaseStats {
	public int hp;
	public int attack;
	public int defense;
	public int specAttack;
	public int specDefense;
	public int speed;
}