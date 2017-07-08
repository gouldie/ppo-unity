using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour {

    public int victor = -1; // 0 = player, 1 = opponent, 2 = tie
    private bool trainerBattle;

    private DialogBoxHandler Dialog;

    private GameObject background;
    public Image playerBase;
    public Image opponentBase;
    public GameObject OptionBox;

    private Pokemon[] pokemon = new Pokemon[6];
    private string[][] pokemonMoveset = new string[][] {
        new string[4],
        new string[4],
        new string[4],
        new string[4],
        new string[4],
        new string[4]
    };

    private int taskSelected; // -1 = invisible, 0 = none, 1 = fight, 2 = pokemon, 3 = bag, 4 = flee
    private int moveSelected; // 1-4

    void Awake() {
        Dialog = transform.GetComponent<DialogBoxHandler>();
    }

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    /// Basic Wild Battle
    public IEnumerator control(Pokemon wildPokemon) {
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
        // ...

        victor = -1;

        // Reset position variables
        // ...
        setSelectedTask(-1);

        bool running = true;
        bool runState = true;

        switchPokemon(3, opponentParty[0], false, false);

        if (trainerBattle) {

        } else {
            StartCoroutine(BattleScreenFade.main.FadeCutout(1.5f));
            StartCoroutine(slidePokemon(opponentBase, false, new Vector2(87,16)));
            yield return StartCoroutine(slidePokemon(playerBase, true, new Vector2(-84,-58)));

            Dialog.DrawDialogBox();
            StartCoroutine(Dialog.DrawTextSilent("A wild " + pokemon[3].getName() + " appeared!"));
            yield return new WaitForSeconds(1.0f);
            Dialog.UndrawDialogBox();

        }

        setSelectedTask(0);

        while (running) {

            runState = true;
            while (runState) {
                if (taskSelected == 0) {
                    OptionBox.SetActive(true);
                }
                else if (taskSelected == 4) {
                    if (trainerBattle) {
                        OptionBox.SetActive(false);
                        Dialog.DrawDialogBox();
                        StartCoroutine(Dialog.DrawTextSilent("No! There's no running from a trainer battle!"));
                        yield return new WaitForSeconds(1.0f);
                        Dialog.UndrawDialogBox();
                        setSelectedTask(0);
                    } else {
                        OptionBox.SetActive(false);
                        Dialog.DrawDialogBox();
                        StartCoroutine(Dialog.DrawTextSilent("Got away safely!"));
                        yield return new WaitForSeconds(1.0f);
                        Dialog.UndrawDialogBox();
                        setSelectedTask(-1);
                        runState = false;
                        running = false;
                    }
                }

                yield return null;
            }
        }

//        yield return null;
        this.gameObject.SetActive(false);
    }

    /// Slide Pokemon across battle screen
    private IEnumerator slidePokemon(Image platform, bool fromRight, Vector2 destinationPos) {
        Vector2 startPosition = (fromRight)
            ? new Vector2(55 + (platform.rectTransform.sizeDelta.x * platform.rectTransform.localScale.x / 2f), destinationPos.y)
            : new Vector2(-55 - (platform.rectTransform.sizeDelta.x * platform.rectTransform.localScale.x / 2f), destinationPos.y);

        Vector2 distance = destinationPos - startPosition;

        float speed = 2.0f;
        float increment = 0f;

        while (increment < 1) {
            increment += (1 / speed) * Time.deltaTime;

            if (increment > 1) {
                increment = 1;
            }

            platform.rectTransform.localPosition = startPosition + (distance * increment);
            yield return null;
        }
        yield return null;
    }

    /// Switch Pokemon
    private bool switchPokemon(int switchPosition, Pokemon newPokemon) {
        return switchPokemon(switchPosition, newPokemon, false, false);
    }

    /// Switch Pokemon
    private bool switchPokemon(int switchPosition, Pokemon newPokemon, bool batonPass) {
        return switchPokemon(switchPosition, newPokemon, batonPass, false);
    }

    /// Switch Pokemon
    private bool switchPokemon(int switchPokemon, Pokemon newPokemon, bool batonPass, bool forceSwitch) {
        if (newPokemon == null) {
            return false;
        }

        if (newPokemon.getStatus() == Pokemon.Status.FAINTED) {
            return false;
        }

        // Implement forceSwitch later

        pokemon[switchPokemon] = newPokemon;
        pokemonMoveset[switchPokemon] = newPokemon.getMoveset();

        // Set PokemonData
        // ...

        return true;
    }

    /// Set the state of Option Box
    public void setSelectedTask(int task) {
        taskSelected = task;
    }
}
