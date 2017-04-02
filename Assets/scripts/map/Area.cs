using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

	public Pokemon[] grassCommon;
	public Pokemon[] grassUncommon;
	public Pokemon[] grassRare;
	public Pokemon[] grassVeryRare;
	public Pokemon[] grassExtremelyRare;

	public Pokemon GetWildGrassPokemon(PokemonRarity rarity) {
		if (rarity == PokemonRarity.Uncommon && grassUncommon.Length > 0) {
			return grassUncommon [Random.Range (0, grassUncommon.Length)];
		}
		if (rarity == PokemonRarity.Rare && grassRare.Length > 0) {
			return grassRare [Random.Range (0, grassRare.Length)];
		}
		if (rarity == PokemonRarity.VeryRare && grassVeryRare.Length > 0) {
			return grassVeryRare [Random.Range (0, grassVeryRare.Length)];
		}
		if (rarity == PokemonRarity.ExtremelyRare && grassExtremelyRare.Length > 0) {
			return grassExtremelyRare [Random.Range (0, grassExtremelyRare.Length)];
		}	
		return grassCommon [Random.Range (0, grassCommon.Length)];
	}
}

public enum AreaList {
	PalletTown
}