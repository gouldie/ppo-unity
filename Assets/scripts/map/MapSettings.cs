using System.Collections;
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

    public int GetRandomEncounter(EncounterTypes type) {

        float roll = Random.Range(0.0f,1.0f);
        WildPokemon match = null;

        // Set the match variable
        if (type == EncounterTypes.Roam) {
            if (roll < global.extremelyRareEncounterRate) {
                if (roamExtremelyRare.Length > 0) {
                    match = roamExtremelyRare[Random.Range(0, roamExtremelyRare.Length - 1)];
                }
            }
            else if (roll < global.veryRareEncounterRate) {
                if (roamVeryRare.Length > 0) {
                    match = roamVeryRare[Random.Range(0, roamVeryRare.Length - 1)];
                }
            }
            else if (roll < global.rareEncounterRate) {
                if (roamRare.Length > 0) {
                    match = roamRare[Random.Range(0, roamRare.Length - 1)];
                }
            }
            else if (roll < global.uncommonEncounterRate) {
                if (roamUncommon.Length > 0) {
                    match = roamUncommon[Random.Range(0, roamUncommon.Length - 1)];
                }
            }
            else match = roamCommon[Random.Range(0, roamCommon.Length - 1)];
        }

        if (match == null) match = roamCommon[Random.Range(0, roamCommon.Length - 1)];

        Debug.Log("Pokemon found:");
        Debug.Log(match.ID);

        return 1;
    }
}

[System.Serializable]
public class WildPokemon {
    public int ID;
    public int minLevel;
    public int maxLevel;
}
