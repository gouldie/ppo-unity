using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    public int victor = -1; // 0 = player, 1 = opponent, 2 = tie
    private bool trainerBattle;

    private DialogBoxHandler Dialog;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//    public void setRunning() {
//        run = true;
//	}

//    public IEnumerator control() {
//        while (!run) {
//            yield return null;
//        }
//
//        Scene.main.Battle.gameObject.SetActive(false);
//        run = false;
//    }

    /// Basic Wild Battle
    public IEnumerator control(Pokemon wildPokemon)
    {
        Debug.Log("Encountered:" + wildPokemon.getName());
        yield return StartCoroutine(control(false, new Trainer(new Pokemon[] {wildPokemon}), false));
    }

    /// Main control func
    public IEnumerator control(bool isTrainerBattle, Trainer trainer, bool healedOnDefeat) {
        // Set this up later
        int[] initialLevels = new int[6];

        trainerBattle = isTrainerBattle;
        Pokemon[] opponentParty = trainer.GetParty();
        string opponentName = trainer.GetName();

        // Get battle backgrounds


        yield return null;
    }
}
