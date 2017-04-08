using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonData {

    public enum Type {
        NONE,
        NORMAL,
        FIGHTING,
        FLYING,
        POISON,
        GROUND,
        ROCK,
        BUG,
        GHOST,
        STEEL,
        FIRE,
        WATER,
        GRASS,
        ELECTRIC,
        PSYCHIC,
        ICE,
        DRAGON,
        DARK,
        FAIRY
    }

	private int ID;
    private string name;
    private Type type1;
    private Type type2;
    private string ability1;
    private string ability2;
    private string hiddenAbility;
    private float maleRatio;
    private int catchRate;
	private float height;
    private float weight;
    private int baseExpYield;

    private int evYieldHP;
    private int evYieldATK;
    private int evYieldDEF;
    private int evYieldSPA;
    private int evYieldSPD;
    private int evYieldSPE;

    private int baseFriendship;
    private string species;
    private string pokedexEntry;

    private int baseHP;
    private int baseATK;
    private int baseDEF;
    private int baseSPA;
    private int baseSPD;
    private int baseSPE;

    private int[] movesetLevels;
    private string[] movesetMoves;

    private string[] tmList;

    private int[] evolutionID;
    private string[] evolutionRequirements;

    public PokemonData(int ID, string name, Type type1, Type type2, string ability1, string ability2, string hiddenAbility,
    					float maleRatio, int catchRate, float height, float weight, int baseExpYield, int evYieldHP,
            			int evYieldATK, int evYieldDEF, int evYieldSPA, int evYieldSPD, int evYieldSPE, int baseFriendship,
    					string species, string pokedexEntry, int baseHP, int baseATK, int baseDEF, int baseSPA, int baseSPD,
    					int baseSPE, int[] movesetLevels, string[] movesetMoves, string[] tmList, int[] evolutionID, string[] evolutionRequirements) {
        this.ID = ID;
        this.name = name;
        this.type1 = type1;
        this.type2 = type2;
        this.ability1 = ability1;
        this.ability2 = ability2;
        this.hiddenAbility = hiddenAbility;

        this.maleRatio = maleRatio;
        this.catchRate = catchRate;

        this.height = height;
        this.weight = weight;
        this.baseExpYield = baseExpYield;

        this.evYieldHP = evYieldHP;
        this.evYieldATK = evYieldATK;
        this.evYieldDEF = evYieldDEF;
        this.evYieldSPA = evYieldSPA;
        this.evYieldSPD = evYieldSPD;
        this.evYieldSPE = evYieldSPE;

        this.baseFriendship = baseFriendship;
        this.species = species;
        this.pokedexEntry = pokedexEntry;

        this.baseHP = baseHP;
        this.baseATK = baseATK;
        this.baseDEF = baseDEF;
        this.baseSPA = baseSPA;
        this.baseSPD = baseSPD;
        this.baseSPE = baseSPE;

        this.movesetLevels = movesetLevels;
        this.movesetMoves = movesetMoves;
        this.tmList = tmList;

        this.evolutionID = evolutionID;
        this.evolutionRequirements = evolutionRequirements;
    }

    public override string ToString() {
        string result = ID.ToString() + ", " + name + ", " + type1.ToString() + ", ";
        result += type2.ToString();
        result += ability1 + ", ";
        if (ability2 != null) {
            result += ability2 + ", ";
        }
        result += hiddenAbility + ", ";
        return result;
    }

    public int getID() {
        return ID;
    }

    public string getName() {
        return name;
    }

    public float getMaleRatio() {
        return maleRatio;
    }

    public PokemonData.Type getType1() {
        return type1;
    }

    public PokemonData.Type getType2() {
        return type2;
    }

    public string getAbility(int ability) {
        if (ability == 0) {
            return ability1;
        }

        if (ability == 1) {
            if (ability2 == null) {
                return ability1;
            }
            return ability2;
        }

        if (ability == 2) {
            if (hiddenAbility == null) {
                return ability1;
            }
            return hiddenAbility;
        }

        return ability1;
    }

    public int getBaseFriendship() {
        return baseFriendship;
    }

    public int getBaseExpYield() {
        return baseExpYield;
    }

    public int[] getBaseStats() {
        return new int[] {baseHP, baseATK, baseDEF, baseSPA, baseSPD, baseSPE};
    }

    public int[] getMovesetLevels() {
        return movesetLevels;
    }

    public string[] getMovesetMoves() {
        return movesetMoves;
    }

    public string[] getTmList() {
        return tmList;
    }

    public int[] getEvolutions() {
        return evolutionID;
    }

    public string[] getEvolutionRequirements() {
        return evolutionRequirements;
    }

    public int getCatchRate() {
        return catchRate;
    }

    public string[] generateMoveset(int level) {
        string[] moveset = new string[4];
        int i = movesetLevels.Length - 1;

        while (moveset[3] == null) {
            if (movesetLevels[i] <= level) {
                moveset[3] = movesetMoves[i];
            }
            i -= 1;
        }
        if (i >= 0) {
            moveset[2] = movesetMoves[i];
            i -= 1;
            if (i >= 0) {
                moveset[1] = movesetMoves[i];
                i -= 1;
                if (i >= 0) {
                    moveset[0] = movesetMoves[i];
                    i -= 1;
                }
            }
        }

        i = 0;
        int i2 = 0;

        if (moveset[0] == null) {
            while (i < 3) {
                while (moveset[i] == null) {
                    i += 1;
                }
                moveset[i2] = moveset[i];
                moveset[i] = null;
                i2 += 1;
            }
        }

        return moveset;
    }
}
