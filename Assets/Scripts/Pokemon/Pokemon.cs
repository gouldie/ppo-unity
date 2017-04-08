using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon : MonoBehaviour {

    public enum Status {
        NONE,
        BURNED,
        FROZEN,
        PARALYZED,
        POISONED,
        ASLEEP,
        FAINTED
    }

    public enum Gender {
        NONE,
        MALE,
        FEMALE,
        CALCULATE
    }

    public enum Ball {
        POKE,
        GREAT,
        ULTRA
    }

    private int pokemonID;
    private string nickname;
    private int form;
    private Gender gender;
    private int level;
    private int exp;
    private int nextLevelExp;
    private int friendship;
    private int wildRarity; // ???
    private bool isShiny;
    private Status status;
    private int sleepTurns;
    private Ball caughtBall;
    private string heldItem;
    private string metDate;
    private string metMap;
    private int metLevel;
    private string OT;
    private int IDno;
    private int ability;
    private string nature;

    private int IV_HP;
    private int IV_ATK;
    private int IV_DEF;
    private int IV_SPA;
    private int IV_SPD;
    private int IV_SPE;

    private int EV_HP;
    private int EV_ATK;
    private int EV_DEF;
    private int EV_SPA;
    private int EV_SPD;
    private int EV_SPE;

    private int HP;
    private int currentHP;
    private int ATK;
    private int DEF;
    private int SPA;
    private int SPD;
    private int SPE;

    private string[] moveset;
    private string[] moveHistory;
    private int[] PPups;
    private int[] maxPP;
    private int[] PP;

    // All details
	public Pokemon(int pokemonID, string nickname, Gender gender, int level, bool isShiny, string caughtBall, string heldItem, string OT,
                    int IV_HP, int IV_ATK, int IV_DEF, int IV_SPA, int IV_SPD, int IV_SPE, int EV_HP, int EV_ATK, int EV_DEF, int EV_SPA, int EV_SPD, int EV_SPE,
                    string nature, int ability, string[] moveset, int[] PPups) {

        //PokemonData thisPokemonData =
    }
}
