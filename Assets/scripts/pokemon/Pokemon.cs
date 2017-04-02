using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour {

	public Sprite image;
	public PokemonName name;
	public PokemonType type;
	public PokemonEvolution evolution;
	public BaseStats baseStats;

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
	Ivysaur,
	Venusaur,
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