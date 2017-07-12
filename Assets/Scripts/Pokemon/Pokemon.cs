﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon {

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

    // New pokemon with all details
	public Pokemon(int pokemonID, string nickname, Gender gender, int level, bool isShiny, Ball caughtBall, string heldItem, string OT,
                    int IV_HP, int IV_ATK, int IV_DEF, int IV_SPA, int IV_SPD, int IV_SPE, int EV_HP, int EV_ATK, int EV_DEF, int EV_SPA, int EV_SPD, int EV_SPE,
                    string nature, int ability, string[] moveset, int[] PPups) {

        PokemonData thisPokemonData = PokemonDatabase.getPokemon(pokemonID);

        this.pokemonID = pokemonID;
        this.nickname = nickname;
        this.form = 0;
        this.gender = gender;

        if (gender == Gender.CALCULATE) {
            if (thisPokemonData.getMaleRatio() < 0) {
                this.gender = Gender.NONE;
            }
            else if (Random.Range(0f, 100f) <= thisPokemonData.getMaleRatio()) {
                this.gender = Gender.MALE;
            } else {
                this.gender = Gender.FEMALE;
            }
        }

        this.level = level;
        this.exp = PokemonDatabase.getLevelExp(level);
        this.nextLevelExp = PokemonDatabase.getLevelExp(level + 1);
        this.friendship = thisPokemonData.getBaseFriendship();
        this.isShiny = isShiny;
        this.status = Status.NONE;
        this.sleepTurns = 0;
        this.caughtBall = caughtBall;
        this.heldItem = heldItem;
        this.OT = (string.IsNullOrEmpty(OT) ? "Unknown" : OT);
        this.metLevel = level;
        this.metMap = "Somewhere";
        this.metDate = System.DateTime.Today.Day + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year;

        this.IV_HP = IV_HP;
        this.IV_ATK = IV_ATK;
        this.IV_DEF = IV_DEF;
        this.IV_SPA = IV_SPA;
        this.IV_SPD = IV_SPD;
        this.IV_SPE = IV_SPE;

        this.EV_HP = EV_HP;
        this.EV_ATK = EV_ATK;
        this.EV_DEF = EV_DEF;
        this.EV_SPA = EV_SPA;
        this.EV_SPD = EV_SPD;
        this.EV_SPE = EV_SPE;

        this.nature = nature;
        this.calculateStats();
        this.currentHP = HP;
        this.ability = ability;
        this.moveset = moveset;
        this.moveHistory = moveset;
        this.PPups = PPups;
        this.maxPP = new int[4];
        this.PP = new int[4];

        for (int i = 0; i < 4; i++) {
            if (!string.IsNullOrEmpty(moveset[i])) {
                this.maxPP[i] = Mathf.FloorToInt(MoveDatabase.getMove(moveset[i]).getPP() * ((this.PPups[i] * 0.2f) + 1));
                this.PP[i] = this.maxPP[i];
            }
        }
        packMoveset();
    }

    // New pokemon with random IVs and shininess, default moveset and 0 EVs
    public Pokemon(int pokemonID, Gender gender, int level, Ball caughtBall, string heldItem, string OT, int ability) {
        PokemonData thisPokemonData = PokemonDatabase.getPokemon(pokemonID);

        this.pokemonID = pokemonID;
        this.form = 0;
        this.gender = gender;

        if (gender == Gender.CALCULATE) {
            if (thisPokemonData.getMaleRatio() < 0) {
                this.gender = Gender.NONE;
            }
            else if (Random.Range(0f, 100f) <= thisPokemonData.getMaleRatio()) {
                this.gender = Gender.MALE;
            } else {
                this.gender = Gender.FEMALE;
            }
        }

        this.level = level;
        this.exp = PokemonDatabase.getLevelExp(level);
        this.nextLevelExp = PokemonDatabase.getLevelExp(level + 1);
        this.friendship = thisPokemonData.getBaseFriendship();

        this.isShiny = (Random.Range(0, 65536) < 16);

        this.status = Status.NONE;
        this.sleepTurns = 0;
        this.caughtBall = caughtBall;
        this.heldItem = heldItem;
        this.OT = (string.IsNullOrEmpty(OT) ? "Unknown" : OT);
        this.metLevel = level;
        this.metMap = "Somewhere";
        this.metDate = System.DateTime.Today.Day + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year;

        this.IV_HP = Random.Range(0, 32);
        this.IV_ATK = Random.Range(0, 32);
        this.IV_DEF = Random.Range(0, 32);
        this.IV_SPA = Random.Range(0, 32);
        this.IV_SPD = Random.Range(0, 32);
        this.IV_SPE = Random.Range(0, 32);

        this.EV_HP = 0;
        this.EV_ATK = 0;
        this.EV_DEF = 0;
        this.EV_SPA = 0;
        this.EV_SPD = 0;
        this.EV_SPE = 0;

        this.nature = NatureDatabase.getRandomNature().getName();
        this.calculateStats();
        this.currentHP = HP;

        if (ability < 0 || ability > 2) {
            this.ability = Random.Range(0,2);
        } else {
            this.ability = ability;
        }

        this.moveset = thisPokemonData.generateMoveset(this.level);
        this.moveHistory = moveset;

        this.PPups = new int[4];
        this.maxPP = new int[4];
        this.PP = new int[4];

        for (int i = 0; i < 4; i++) {
            if (!string.IsNullOrEmpty(moveset[i])) {
                this.maxPP[i] = MoveDatabase.getMove(this.moveset[i]).getPP();
                this.PP[i] = this.maxPP[i];
            }
        }
        packMoveset();
    }

    // Caught pokemon, only a few customizable details
    public Pokemon(Pokemon pokemon, string nickname, Ball caughtBall) {
        PokemonData thisPokemonData = PokemonDatabase.getPokemon(pokemonID);

        this.pokemonID = pokemon.pokemonID;
        this.nickname = nickname;
        this.form = 0;
        this.gender = pokemon.gender;


        this.level = pokemon.level;
        this.exp = pokemon.exp;
        this.nextLevelExp = pokemon.nextLevelExp;
        this.friendship = pokemon.friendship;

        this.isShiny = pokemon.isShiny;

        this.status = pokemon.status;
        this.sleepTurns = pokemon.sleepTurns;
        this.caughtBall = caughtBall;
        this.heldItem = pokemon.heldItem;

        this.OT = "Unknown";
        this.metLevel = level;
        this.metMap = "Somewhere";
        this.metDate = System.DateTime.Today.Day + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year;

        this.IV_HP = pokemon.IV_HP;
        this.IV_ATK = pokemon.IV_ATK;
        this.IV_DEF = pokemon.IV_DEF;
        this.IV_SPA = pokemon.IV_SPA;
        this.IV_SPD = pokemon.IV_SPD;
        this.IV_SPE = pokemon.IV_SPE;

        this.EV_HP = pokemon.EV_HP;
        this.EV_ATK = pokemon.EV_ATK;
        this.EV_DEF = pokemon.EV_DEF;
        this.EV_SPA = pokemon.EV_SPA;
        this.EV_SPD = pokemon.EV_SPD;
        this.EV_SPE = pokemon.EV_SPE;

        this.nature = pokemon.nature;
        this.calculateStats();
        this.currentHP = pokemon.currentHP;
        this.ability = pokemon.ability;

        this.moveset = pokemon.moveset;
        this.moveHistory = pokemon.moveHistory;

        this.PPups = pokemon.PPups;
        this.maxPP = new int[4];
        this.PP = new int[4];

        for (int i = 0; i < 4; i++) {
            if (!string.IsNullOrEmpty(moveset[i])) {
                this.maxPP[i] = Mathf.FloorToInt(MoveDatabase.getMove(moveset[i]).getPP() * ((this.PPups[i] * 0.2f) + 1));
                this.PP[i] = this.maxPP[i];
            }
        }
        packMoveset();
    }

    public void calculateStats() {
        int[] baseStats = PokemonDatabase.getPokemon(pokemonID).getBaseStats();
        if (baseStats[0] == 1)
        {
            this.HP = 1;
        }
        else
        {
            int prevMaxHP = this.HP;
            this.HP = Mathf.FloorToInt(((IV_HP + (2 * baseStats[0]) + (EV_HP / 4) + 100) * level) / 100 + 10);
            this.currentHP = (this.currentHP + (this.HP - prevMaxHP) < this.HP)
            ? this.currentHP + (this.HP - prevMaxHP)
            : this.HP;
        }
        if (baseStats[1] == 1)
        {
            this.ATK = 1;
        }
        else
        {
            this.ATK =
            Mathf.FloorToInt(Mathf.FloorToInt(((IV_ATK + (2 * baseStats[1]) + (EV_ATK / 4)) * level) / 100 + 5) *
            NatureDatabase.getNature(nature).getATK());
        }
        if (baseStats[2] == 1)
        {
            this.DEF = 1;
        }
        else
        {
            this.DEF =
            Mathf.FloorToInt(Mathf.FloorToInt(((IV_DEF + (2 * baseStats[2]) + (EV_DEF / 4)) * level) / 100 + 5) *
            NatureDatabase.getNature(nature).getDEF());
        }
        if (baseStats[3] == 1)
        {
            this.SPA = 1;
        }
        else
        {
            this.SPA =
            Mathf.FloorToInt(Mathf.FloorToInt(((IV_SPA + (2 * baseStats[3]) + (EV_SPA / 4)) * level) / 100 + 5) *
            NatureDatabase.getNature(nature).getSPA());
        }
        if (baseStats[4] == 1)
        {
            this.SPD = 1;
        }
        else
        {
            this.SPD =
            Mathf.FloorToInt(Mathf.FloorToInt(((IV_SPD + (2 * baseStats[4]) + (EV_SPD / 4)) * level) / 100 + 5) *
            NatureDatabase.getNature(nature).getSPD());
        }
        if (baseStats[5] == 1)
        {
            this.SPE = 1;
        }
        else
        {
            this.SPE =
            Mathf.FloorToInt(Mathf.FloorToInt(((IV_SPE + (2 * baseStats[5]) + (EV_SPE / 4)) * level) / 100 + 5) *
            NatureDatabase.getNature(nature).getSPE());
        }
    }

    public void setNickname(string nickname) {
        this.nickname = nickname;
    }

    public string swapHeldItem(string newItem) {
        string oldItem = this.heldItem;
        this.heldItem = newItem;
        return oldItem;
    }

    public void addExp(int expAdded) {
        if (level < 100) {
            this.exp += expAdded;
            while (exp >= nextLevelExp) {
                this.level += 1;
                this.nextLevelExp = PokemonDatabase.getLevelExp(level + 1);
                calculateStats();

                if (this.HP > 0 && this.status == Status.FAINTED) {
                    this.status = Status.NONE;
                }
            }
        }
    }

    public bool addEVs(string stat, float amount)
    {
        int intAmount = Mathf.FloorToInt(amount);
        int evTotal = EV_HP + EV_ATK + EV_DEF + EV_SPA + EV_SPD + EV_SPE;
        if (evTotal < 510)
        {
            //if total EV cap is already reached.
            if (evTotal + intAmount > 510)
            {
                //if this addition will pass the total EV cap.
                intAmount = 510 - evTotal; //set intAmount to be the remaining points before cap is reached.
            }
            if (stat == "HP")
            {
                //if adding to HP.
                if (EV_HP < 252)
                {
                    //if HP is not full.
                    EV_HP += intAmount;
                    if (EV_HP > 252)
                    {
                        //if single stat EV cap is passed.
                        EV_HP = 252;
                    } //set stat back to the cap.
                    return true;
                }
            }
            else if (stat == "ATK")
            {
                //if adding to ATK.
                if (EV_ATK < 252)
                {
                    //if ATK is not full.
                    EV_ATK += intAmount;
                    if (EV_ATK > 252)
                    {
                        //if single stat EV cap is passed.
                        EV_ATK = 252;
                    } //set stat back to the cap.
                    return true;
                }
            }
            else if (stat == "DEF")
            {
                //if adding to DEF.
                if (EV_DEF < 252)
                {
                    //if DEF is not full.
                    EV_DEF += intAmount;
                    if (EV_DEF > 252)
                    {
                        //if single stat EV cap is passed.
                        EV_DEF = 252;
                    } //set stat back to the cap.
                    return true;
                }
            }
            else if (stat == "SPA")
            {
                //if adding to SPA.
                if (EV_SPA < 252)
                {
                    //if SPA is not full.
                    EV_SPA += intAmount;
                    if (EV_SPA > 252)
                    {
                        //if single stat EV cap is passed.
                        EV_SPA = 252;
                    } //set stat back to the cap.
                    return true;
                }
            }
            else if (stat == "SPD")
            {
                //if adding to SPD.
                if (EV_SPD < 252)
                {
                    //if SPD is not full.
                    EV_SPD += intAmount;
                    if (EV_SPD > 252)
                    {
                        //if single stat EV cap is passed.
                        EV_SPD = 252;
                    } //set stat back to the cap.
                    return true;
                }
            }
            else if (stat == "SPE")
            {
                //if adding to SPE.
                if (EV_SPE < 252)
                {
                    //if SPE is not full.
                    EV_SPE += intAmount;
                    if (EV_SPE > 252)
                    {
                        //if single stat EV cap is passed.
                        EV_SPE = 252;
                    } //set stat back to the cap.
                    return true;
                }
            }
        }
        return false; //returns false if total or relevant EV cap was reached before running.
    }

    public int getEvolutionID(string currentMethod) {
        PokemonData thisPokemonData = PokemonDatabase.getPokemon(pokemonID);
        int[] evolutions = thisPokemonData.getEvolutions();
        string[] evolutionRequirements = thisPokemonData.getEvolutionRequirements();

        for (int i = 0; i < evolutions.Length; i++) {
            //if satisfied, return true
            if (checkEvolutionMethods(currentMethod, evolutionRequirements[i])) {
                return evolutions[i];
            }
        }
        return 1;
    }

    //Check PokemonData.cs for list of evolution method names.
    public bool canEvolve(string currentMethod) {
        PokemonData thisPokemonData = PokemonDatabase.getPokemon(pokemonID);
        int[] evolutions = thisPokemonData.getEvolutions();
        string[] evolutionRequirements = thisPokemonData.getEvolutionRequirements();

        for (int i = 0; i < evolutions.Length; i++) {
            //if an evolution method was satisfied, return true
            if (checkEvolutionMethods(currentMethod, evolutionRequirements[i])) {
            //	Debug.Log("All Checks Passed");
                return true;
            }
        }
        //Debug.Log("Check Failed");
        return false;
    }

    //check that the evolution can be
    public bool checkEvolutionMethods(string currentMethod, string evolutionRequirements) {
        string[] evolutionSplit = evolutionRequirements.Split(',');
        string[] methods = evolutionSplit[0].Split('\\');
        string[] currentMethodSplit = currentMethod.Split(',');
        //if currentMethod needs a parameter attached, it will be separated by a ' , '
        string[] parameters = new string[] {};
        if (evolutionSplit.Length > 0) {
            //if true, there is a parameter attached
            parameters = evolutionSplit[1].Split('\\');
        }
        for (int i = 0; i < methods.Length; i++) {
            //for every method for the currently checked evolution
            //Debug.Log(evolutionRequirements +" | "+ currentMethodSplit[0] +" "+ methods[i] +" "+ parameters[i]);
            if (methods[i] == "Level") {
                //if method contains a Level requirement
                if (currentMethodSplit[0] != "Level") {
                    //and system is not checking for a level evolution
                    return false; //cannot evolve. return false and stop checking.
                }
                else
                {
                    if (this.level < int.Parse(parameters[i])) {
                        //and pokemon's level is not high enough to evolve,
                        return false; //cannot evolve. return false stop checking.
                    }
                }
            }
            else if (methods[i] == "Stone") {
                //if method contains a Stone requirement
                if (currentMethodSplit[0] != "Stone") {
                    //and system is not checking for a stone evolution
                    return false; //cannot evolve. return false and stop checking.
                }
                else
                {
                    //if it is checking for a stone evolution,
                    if (currentMethodSplit[1] != parameters[i]) {
                        //and parameter being checked does not match the required one
                        return false; //cannot evolve. return false and stop checking.
                    }
                }
            }
            else if (methods[i] == "Trade") {
                //if method contains a Trade requirement
                if (currentMethodSplit[0] != "Trade") {
                    //and system is not checking for a trade evolution
                    return false; //cannot evolve. return false and stop checking.
                }
            }
            else if (methods[i] == "Friendship") {
                //if method contains a Friendship requirement
                if (this.friendship < 220) {
                    //and pokemon's friendship is less than 220
                    return false; //cannot evolve. return false and stop checking.
                }
            }
            else if (methods[i] == "Item") {
                //if method contains an Item requirement
                if (this.heldItem == parameters[i]) {
                    //and pokemon's Held Item is not the specified Item
                    return false; //cannot evolve. return false and stop checking.
                }
            }
            else if (methods[i] == "Gender") {
                //if method contains a Gender requirement
                if (this.gender.ToString() != parameters[i]) {
                    //and pokemon's gender is not the required gender to evolve,
                    return false; //cannot evolve. return false and stop checking.
                }
            }
            else if (methods[i] == "Move") {
                //if method contains a Move requirement
                if (!hasMove(parameters[i])) {
                    //and pokemon does not have the specified move
                    return false; //cannot evolve. return false and stop checking.
                }
            }
//            else if (methods[i] == "Map") {
//                //if method contains a Map requirement
//                string mapName = PlayerMovement.player.currentMap.name;
//                if (mapName != parameters[i]) {
//                    //and current map is not the required map to evolve,
//                    return false; //cannot evolve. return false and stop checking.
//                }
//            }
            else if (methods[i] == "Time") {
                //if method contains a Time requirement
                string dayNight = "Day";
                if (System.DateTime.Now.Hour >= 21 || System.DateTime.Now.Hour < 4) {
                    //if time is night time
                    dayNight = "Night"; //set dayNight to be "Night"
                }
                if (dayNight != parameters[i]) {
                    //if time is not what the evolution requires (Day/Night)
                    return false; //cannot evolve. return false and stop checking.
                }
            }
            else {
                //if methods[i] did not equal to anything above, methods[i] is an invalid method.
                return false;
            }
        }
        //if the code did not return false once, then the evolution requirements must have been met.
        return true;
    }

    public bool evolve(string currentMethod) {
        int[] evolutions = PokemonDatabase.getPokemon(pokemonID).getEvolutions();
        string[] evolutionRequirements = PokemonDatabase.getPokemon(pokemonID).getEvolutionRequirements();
        for (int i = 0; i < evolutions.Length; i++) {
            if (checkEvolutionMethods(currentMethod, evolutionRequirements[i])) {
                float hpPercent = ((float) this.currentHP / (float) this.HP);
                this.pokemonID = evolutions[i];
                calculateStats();
                i = evolutions.Length;
                currentHP = Mathf.RoundToInt(HP * hpPercent);
                return true;
            }
        }
        return false;
    }

    //return a string that contains all of this pokemon's data
    public override string ToString() {
        string result = pokemonID + ": " + this.getName() + "(" + PokemonDatabase.getPokemon(pokemonID).getName() +
        "), " +
        gender.ToString() + ", Level " + level + ", EXP: " + exp + ", To next: " + (nextLevelExp - exp) +
        ", Friendship: " + friendship + "," +
        ", Shiny=" + isShiny.ToString() +
        ", Status: " + status + ", Ball: " + caughtBall + ", Item: " + heldItem +
        ", met at Level " + metLevel + " on " + metDate + " at " + metMap +
        ", OT: " + OT + ", ID: " + IDno +
        ", IVs: " + IV_HP + "," + IV_ATK + "," + IV_DEF + "," + IV_SPA + "," + IV_SPD + "," + IV_SPE +
        ", EVs: " + EV_HP + "," + EV_ATK + "," + EV_DEF + "," + EV_SPA + "," + EV_SPD + "," + EV_SPE +
        ", Stats: " + currentHP + "/" + HP + "," + ATK + "," + DEF + "," + SPA + "," + SPD + "," + SPE +
        ", Nature: " + nature + ", " + PokemonDatabase.getPokemon(pokemonID).getAbility(ability);
        result += ", [";
        for (int i = 0; i < 4; i++) {
            if (!string.IsNullOrEmpty(moveset[i])) {
                result += moveset[i] + ": " + PP[i] + "/" + maxPP[i] + ", ";
            }
        }
        result = result.Remove(result.Length - 2, 2);
        result += "]";

        return result;
    }

    //Heal the pokemon
    public void healFull() {
        currentHP = HP;
        PP[0] = maxPP[0];
        PP[1] = maxPP[1];
        PP[2] = maxPP[2];
        PP[3] = maxPP[3];
        status = Status.NONE;
    }

    //returns the excess hp
    public int healHP(float amount) {
        int excess = 0;
        int intAmount = Mathf.RoundToInt(amount);
        currentHP += intAmount;
        if (currentHP > HP) {
            excess = currentHP - HP;
            currentHP = HP;
        }
        if (status == Status.FAINTED && currentHP > 0) {
            status = Status.NONE;
        }
        return intAmount - excess;
    }

    public int healPP(int move, float amount) {
        int excess = 0;
        int intAmount = Mathf.RoundToInt(amount);
        this.PP[move] += intAmount;
        if (this.PP[move] > this.maxPP[move]) {
            excess = this.PP[move] - this.maxPP[move];
            this.PP[move] = this.maxPP[move];
        }
        return intAmount - excess;
    }

    public void healStatus() {
        status = Status.NONE;
    }

    public void removeHP(float amount)
    {
        int intAmount = Mathf.RoundToInt(amount);
        this.currentHP -= intAmount;
        if (this.currentHP <= 0)
        {
            this.currentHP = 0;
            this.status = Status.FAINTED;
        }
    }

    public void removePP(string move, float amount) {
        removePP(getMoveIndex(move), amount);
    }

    public void removePP(int move, float amount) {
        if (move >= 0)
        {
            int intAmount = Mathf.RoundToInt(amount);
            this.PP[move] -= intAmount;
            if (this.PP[move] < 0)
            {
                this.PP[move] = 0;
            }
        }
    }

    public bool setStatus(Status status) {
        if (this.status == Status.NONE) {
            this.status = status;
            if (status == Status.ASLEEP) {
                //if applying sleep, set sleeping
                sleepTurns = Random.Range(1, 4);
            } //turns to 1, 2, or 3
            return true;
        }
        else
        {
            if (status == Status.NONE || status == Status.FAINTED) {
                this.status = status;
                sleepTurns = 0;
                return true;
            }
        }
        return false;
    }

    public void removeSleepTurn() {
        if (status == Status.ASLEEP) {
            sleepTurns -= 1;
            if (sleepTurns <= 0) {
                setStatus(Status.NONE);
            }
        }
    }

    public int getSleepTurns() {
        return sleepTurns;
    }

    public string getFirstFEInstance(string moveName) {
        for (int i = 0; i < moveset.Length; i++) {
            if (MoveDatabase.getMove(moveset[i]).getFieldEffect() == moveName) {
                return moveset[i];
            }
        }
        return null;
    }

    public int getID() {
        return pokemonID;
    }

    public string getLongID() {
        string result = pokemonID.ToString();
        while (result.Length < 3) {
            result = "0" + result;
        }
        return result;
    }

    public static string convertLongID(int ID) {
        string result = ID.ToString();
        while (result.Length < 3) {
            result = "0" + result;
        }
        return result;
    }

    //Get the pokemon's nickname, or regular name if it has none.
    public string getName() {
        if (string.IsNullOrEmpty(nickname)) {
            return PokemonDatabase.getPokemon(pokemonID).getName();
        }
        else {
            return nickname;
        }
    }

    public Gender getGender() {
        return gender;
    }

    public int getLevel() {
        return level;
    }

    public int getExp() {
        return exp;
    }

    public int getExpNext() {
        return nextLevelExp;
    }

    public int getFriendship() {
        return friendship;
    }

    public bool getIsShiny() {
        return isShiny;
    }

    public Status getStatus() {
        return status;
    }

    public Ball getCaughtBall() {
        return caughtBall;
    }

    public string getHeldItem() {
        return heldItem;
    }

    public string getMetDate() {
        return metDate;
    }

    public string getMetMap() {
        return metMap;
    }

    public int getMetLevel() {
        return metLevel;
    }

    public string getOT() {
        return OT;
    }

    public int getIDno() {
        return IDno;
    }

    public int GetIV(int index) {
        if (index == 0) {
            return IV_HP;
        }
        else if (index == 1) {
            return IV_ATK;
        }
        else if (index == 2) {
            return IV_DEF;
        }
        else if (index == 3) {
            return IV_SPA;
        }
        else if (index == 4) {
            return IV_SPD;
        }
        else if (index == 5) {
            return IV_SPE;
        }
        return -1;
    }

    public int getIV_HP() {
        return IV_HP;
    }

    public int getIV_ATK() {
        return IV_ATK;
    }

    public int getIV_DEF() {
        return IV_DEF;
    }

    public int getIV_SPA() {
        return IV_SPA;
    }

    public int getIV_SPD() {
        return IV_SPD;
    }

    public int getIV_SPE() {
        return IV_SPE;
    }

    public int getEV_HP() {
        return EV_HP;
    }

    public int getEV_ATK() {
        return EV_ATK;
    }

    public int getEV_DEF() {
        return EV_DEF;
    }

    public int getEV_SPA() {
        return EV_SPA;
    }

    public int getEV_SPD() {
        return EV_SPD;
    }

    public int getEV_SPE() {
        return EV_SPE;
    }

    public string getNature() {
        return nature;
    }

    public int getHP() {
        return HP;
    }

    public int getCurrentHP() {
        return currentHP;
    }

    public float getPercentHP() {
        return 1f - (((float) HP - (float) currentHP) / (float) HP);
    }

    public int getATK() {
        return ATK;
    }

    public int getDEF() {
        return DEF;
    }

    public int getSPA() {
        return SPA;
    }

    public int getSPD() {
        return SPD;
    }

    public int getSPE() {
        return SPE;
    }

    public int getAbility() {
        return ability;
    }

    public int getMoveIndex(string move) {
        for (int i = 0; i < moveset.Length; i++) {
            if (!string.IsNullOrEmpty(moveset[i])) {
                if (moveset[i] == move) {
                    return i;
                }
            }
        }
        return -1;
    }

    public string[] getMoveset() {
        string[] result = new string[4];
        for (int i = 0; i < 4; i++) {
            result[i] = moveset[i];
        }
        return result;
    }

    public void swapMoves(int target1, int target2) {
        string temp = moveset[target1];
        this.moveset[target1] = moveset[target2];
        this.moveset[target2] = temp;
    }


    private void ResetPP(int index) {
        PPups[index] = 0;
        maxPP[index] = Mathf.FloorToInt(MoveDatabase.getMove(moveset[index]).getPP() * ((PPups[index] * 0.2f) + 1));
        PP[index] = maxPP[index];
    }

    // Returns false if no room to add the new move OR move already is learned.
    public bool addMove(string newMove) {
        if (!hasMove(newMove) && string.IsNullOrEmpty(moveset[3])) {
            moveset[3] = newMove;
            ResetPP(3);
            packMoveset();
            return true;
        }
        return false;
    }

    public void replaceMove(int index, string newMove) {
        if (index >= 0 && index < 4) {
            moveset[index] = newMove;
            addMoveToHistory(newMove);
            ResetPP(index);
        }
    }

    // Returns false if only one move is left in the moveset.
    public bool forgetMove(int index) {
        if (getMoveCount() > 1) {
            moveset[index] = null;
            packMoveset();
            return true;
        }
        return false;
    }

    public int getMoveCount() {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (!string.IsNullOrEmpty(moveset[i]))
            {
                count += 1;
            }
        }
        return count;
    }

    private void packMoveset() {
        string[] packedMoveset = new string[4];
        int[] packedPP = new int[4];
        int[] packedMaxPP = new int[4];
        int[] packedPPups = new int[4];

        int i2 = 0; //counter for packed array
        for (int i = 0; i < 4; i++) {
            if (!string.IsNullOrEmpty(moveset[i])) {
                //if next move in moveset is not null
                packedMoveset[i2] = moveset[i]; //add to packed moveset
                packedPP[i2] = PP[i];
                packedMaxPP[i2] = maxPP[i];
                packedPPups[i2] = PPups[i];
                i2 += 1;
            } //ready packed moveset's next position
        }
        moveset = packedMoveset;
        PP = packedPP;
        maxPP = packedMaxPP;
        PPups = packedPPups;
    }

    public bool hasMove(string move) {
        if (string.IsNullOrEmpty((move))) {
            return false;
        }
        for (int i = 0; i < moveset.Length; i++) {
            if (moveset[i] == move) {
                return true;
            }
        }
        return false;
    }

    private void addMoveToHistory(string move) {
        if (!HasMoveInHistory(move)) {
            string[] newHistory = new string[moveHistory.Length + 1];
            for (int i = 0; i < moveHistory.Length; i++) {
                newHistory[i] = moveHistory[i];
            }
            newHistory[moveHistory.Length] = move;
            moveHistory = newHistory;
        }
    }

    public bool HasMoveInHistory(string move) {
        for (int i = 0; i < moveset.Length; i++) {
            if (moveset[i] == move) {
                return true;
            }
        }
        return false;
    }

    public bool CanLearnMove(string move) {
        PokemonData thisPokemonData = PokemonDatabase.getPokemon(pokemonID);

        string[] moves = thisPokemonData.getMovesetMoves();
        for (int i = 0; i < moves.Length; i++) {
            if (moves[i] == move) {
                return true;
            }
        }
        moves = thisPokemonData.getTmList();
        for (int i = 0; i < moves.Length; i++) {
            if (moves[i] == move) {
                return true;
            }
        }
        return false;
    }

    public string MoveLearnedAtLevel(int level) {
        PokemonData thisPokemonData = PokemonDatabase.getPokemon(pokemonID);

        int[] movesetLevels = thisPokemonData.getMovesetLevels();
        for (int i = 0; i < movesetLevels.Length; i++) {
            if (movesetLevels[i] == level) {
                return thisPokemonData.getMovesetMoves()[i];
            }
        }
        return null;
    }

    public int[] getPPups() {
        return PPups;
    }

    public int[] getMaxPP() {
        return maxPP;
    }

    public int[] getPP() {
        return PP;
    }

    public int getPP(int index) {
        return PP[index];
    }

    /// SPRITES ///
    public Sprite[] GetFrontAnim_() {
        return GetAnimFromID_("PokemonSprites", pokemonID, gender, isShiny);
    }

    public Sprite[] GetBackAnim_() {
        return GetAnimFromID_("PokemonBackSprites", pokemonID, gender, isShiny);
    }


    public static Sprite[] GetFrontAnimFromID_(int ID, Gender gender, bool isShiny) {
        return GetAnimFromID_("PokemonSprites", ID, gender, isShiny);
    }

    public static Sprite[] GetBackAnimFromID_(int ID, Gender gender, bool isShiny) {
        return GetAnimFromID_("PokemonBackSprites", ID, gender, isShiny);
    }

    private static Sprite[] GetAnimFromID_(string folder, int ID, Gender gender, bool isShiny) {
        Sprite[] animation;
        string shiny = (isShiny) ? "s" : "";
        if (gender == Gender.FEMALE) {
            //Attempt to load Female Variant
            animation = Resources.LoadAll<Sprite>(folder + "/" + convertLongID(ID) + "f" + shiny + "/");
            if (animation.Length == 0) {
                Debug.LogWarning("Female Variant NOT Found");
                //Attempt to load Base Variant (possibly Shiny)
                animation = Resources.LoadAll<Sprite>(folder + "/" + convertLongID(ID) + shiny + "/");
            }
            //	else{ Debug.Log("Female Variant Found"); }
        }
        else {
            //Attempt to load Base Variant (possibly Shiny)
            animation = Resources.LoadAll<Sprite>(folder + "/" + convertLongID(ID) + shiny + "/");
        }
        if (animation.Length == 0 && isShiny) {
            Debug.LogWarning("Shiny Variant NOT Found");
            //No Shiny Variant exists, Attempt to load Regular Variant
            animation = Resources.LoadAll<Sprite>(folder + "/" + convertLongID(ID) + "/");
        }
        return animation;
    }


    public Sprite[] GetIcons_() {
        return GetIconsFromID_(pokemonID, isShiny);
    }

    public static Sprite[] GetIconsFromID_(int ID, bool isShiny) {
        string shiny = (isShiny) ? "s" : "";
        Sprite[] icons = Resources.LoadAll<Sprite>("PokemonIcons/icon" + convertLongID(ID) + shiny);
        if (icons == null) {
            Debug.LogWarning("Shiny Variant NOT Found");
            icons = Resources.LoadAll<Sprite>("PokemonIcons/icon" + convertLongID(ID));
        }
        return icons;
    }

    public float GetCryPitch() {
        return (status == Pokemon.Status.FAINTED) ? 0.9f : 1f - (0.06f * (1 - getPercentHP()));
    }

    public AudioClip GetCry() {
        return GetCryFromID(pokemonID);
    }

    public static AudioClip GetCryFromID(int ID) {
        return Resources.Load<AudioClip>("Audio/cry/" + convertLongID(ID));
    }


    public Sprite GetFrontSprite() {
        return GetSpriteFromID("PokemonFrontSprites", pokemonID, gender, isShiny);
    }

    public Sprite GetBackSprite() {
        return GetSpriteFromID("PokemonBackSprites", pokemonID, gender, isShiny);
    }

    public Texture GetIcons() {
        return GetIconsFromID(pokemonID, isShiny);
    }


    public static Sprite GetFrontSpriteFromID(int ID, Gender gender, bool isShiny) {
        return GetSpriteFromID("PokemonFrontSprites", ID, gender, isShiny);
    }

    public static Sprite GetBackSpriteFromID(int ID, Gender gender, bool isShiny) {
        return GetSpriteFromID("PokemonBackSprites", ID, gender, isShiny);
    }

    private static Sprite GetSpriteFromID(string folder, int ID, Gender gender, bool isShiny) {
        Sprite sprite;
        string shiny = (isShiny) ? "s" : "";

        if (gender == Gender.FEMALE) {
            //Attempt to load Female Variant
            sprite = Resources.Load<Sprite>(folder + "/" + convertLongID(ID) + "f" + shiny);
            if (sprite == null) {
                Debug.LogWarning("Female Variant NOT Found (may not be required)");
                //Attempt to load Base Variant (possibly Shiny)
                sprite = Resources.Load<Sprite>(folder + "/" + convertLongID(ID) + shiny);
            }
        //	else{ Debug.Log("Female Variant Found");}
        }
        else
        {
            //Attempt to load Base Variant (possibly Shiny)
            sprite = Resources.Load<Sprite>(folder + "/" + convertLongID(ID) + shiny);
        }
        if (sprite == null && isShiny) {
            Debug.LogWarning("Shiny Variant NOT Found");
            //No Shiny Variant exists, Attempt to load Regular Variant
            sprite = Resources.Load<Sprite>(folder + "/" + convertLongID(ID));
        }
        return sprite;
    }

    public static Texture GetIconsFromID(int ID, bool isShiny) {
        string shiny = (isShiny) ? "s" : "";
        Texture icons = Resources.Load<Texture>("PokemonIcons/icon" + convertLongID(ID) + shiny);
        if (icons == null) {
            Debug.LogWarning("Shiny Variant NOT Found");
            icons = Resources.Load<Texture>("PokemonIcons/icon" + convertLongID(ID));
        }
        return icons;
    }


    public static Sprite[] GetOverworldSpriteFromID(int ID, bool isShiny, bool getLight) {
        string shiny = (isShiny) ? "s" : "";
        string light = (getLight) ? "Lights/" : "";
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("OverworldPokemonSprites/" + light + convertLongID(ID) + shiny);
        if (spriteSheet.Length == 0) {
            //No Light found AND/OR No Shiny found, load non-shiny
            if (isShiny) {
                if (getLight) {
                    Debug.LogWarning("Shiny Light NOT Found (may not be required)");
                }
                else
                {
                    Debug.LogWarning("Shiny Variant NOT Found");
                }
            }
            spriteSheet = Resources.LoadAll<Sprite>("OverworldPokemonSprites/" + light + convertLongID(ID));
        }
        if (spriteSheet.Length == 0) {
            //No Light found OR No Sprite found, return 8 blank sprites
            if (!getLight)
            {
                Debug.LogWarning("Sprite NOT Found");
            }
            else
            {
                Debug.LogWarning("Light NOT Found (may not be required)");
            }
            return new Sprite[8];
        }
        return spriteSheet;
    }
}
