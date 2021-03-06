﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PokemonRarity {
    Common,
    Uncommon,
    Rare,
    VeryRare,
    ExtremelyRare
}

public enum EncounterTypes {
    Roam,
    Fish,
    Surf
}

public class MapSettings : MonoBehaviour {

    public GlobalVariables global;

    public AudioClip backgroundMusic;
    public string mapName;

    public WildPokemon[] roamCommon;
    public WildPokemon[] roamUncommon;
    public WildPokemon[] roamRare;
    public WildPokemon[] roamVeryRare;
    public WildPokemon[] roamExtremelyRare;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public AudioClip GetBackgroundMusic() {
        return backgroundMusic;
    }

    public Sprite GetBattleBackground() {
        return Resources.Load<Sprite>("BattleBackgrounds/Backgrounds/Field");
    }

    public Sprite GetBattleBase() {
        return Resources.Load<Sprite>("BattleBackgrounds/Bases/Field");
    }

    public Pokemon GetRandomEncounter(EncounterTypes type) {

        float roll = Random.Range(0.0f,1.0f);
        WildPokemon match = null;

        // Set the match variable
        if (type == EncounterTypes.Roam) {
            if (roll < global.extremelyRareEncounterRate) {
                if (roamExtremelyRare.Length > 0) {
                    match = roamExtremelyRare[Random.Range(0, roamExtremelyRare.Length)];
                }
            }
            else if (roll < global.veryRareEncounterRate) {
                if (roamVeryRare.Length > 0) {
                    match = roamVeryRare[Random.Range(0, roamVeryRare.Length)];
                }
            }
            else if (roll < global.rareEncounterRate) {
                if (roamRare.Length > 0) {
                    match = roamRare[Random.Range(0, roamRare.Length)];
                }
            }
            else if (roll < global.uncommonEncounterRate) {
                if (roamUncommon.Length > 0) {
                    match = roamUncommon[Random.Range(0, roamUncommon.Length)];
                }
            }
            else match = roamCommon[Random.Range(0, roamCommon.Length)];
        }

        if (match == null) match = roamCommon[Random.Range(0, roamCommon.Length)];

        return new Pokemon(
                match.ID,
                Pokemon.Gender.CALCULATE,
                Random.Range(match.minLevel, match.maxLevel + 1),
                Pokemon.Ball.POKE, null, null, -1);
    }
}

[System.Serializable]
public class WildPokemon {
    public int ID;
    public int minLevel;
    public int maxLevel;
}
