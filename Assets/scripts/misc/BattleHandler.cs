using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour {

    public int victor = -1; // 0 = player, 1 = opponent, 2 = tie
    private bool trainerBattle;

    private DialogBoxHandler Dialog;

    private GameObject background;
    public Image playerBase;
    public Image opponentBase;

    private Sprite opponent1Sprite;

    public GameObject OptionBox;
    private GameObject PokemonSelectionBox;
    private GameObject Pokemon1Button;
    private GameObject Pokemon2Button;
    private GameObject Pokemon3Button;
    private GameObject Pokemon4Button;
    private GameObject Pokemon5Button;
    private GameObject Pokemon6Button;

//    private Pokemon[] pokemon = new Pokemon[6];
//    private string[][] pokemonMoveset = new string[][] {
//        new string[4],
//        new string[4],
//        new string[4],
//        new string[4],
//        new string[4],
//        new string[4]
//    };

    private int taskSelected; // -1 = invisible, 0 = none, 1 = fight, 2 = pokemon, 3 = bag, 4 = flee
    private int moveSelected; // 1-4
    private int pokemonSelected; // 1-6

    private Pokemon[] playerParty = new Pokemon[6]; // temp until localstorage / serverstorage working

    private Pokemon playerActivePokemon;
    private Pokemon enemyActivePokemon;
    private string[] playerActivePokemonMoveset;
    private string[] enemyActivePokemonMoveset;

    void Awake() {
        Dialog = transform.GetComponent<DialogBoxHandler>();

        PokemonSelectionBox = transform.FindChild("PokemonSelectionBox").gameObject;
        Pokemon1Button = PokemonSelectionBox.transform.FindChild("Pokemon 1 Button").gameObject;
        Pokemon2Button = PokemonSelectionBox.transform.FindChild("Pokemon 2 Button").gameObject;
        Pokemon3Button = PokemonSelectionBox.transform.FindChild("Pokemon 3 Button").gameObject;
        Pokemon4Button = PokemonSelectionBox.transform.FindChild("Pokemon 4 Button").gameObject;
        Pokemon5Button = PokemonSelectionBox.transform.FindChild("Pokemon 5 Button").gameObject;
        Pokemon6Button = PokemonSelectionBox.transform.FindChild("Pokemon 6 Button").gameObject;

        playerParty[0] = new Pokemon(4, Pokemon.Gender.MALE, 5,  Pokemon.Ball.POKE, "None", "Gouldie", 0);
        playerParty[1] = new Pokemon(1, Pokemon.Gender.MALE, 5,  Pokemon.Ball.POKE, "None", "Gouldie", 0);
        playerParty[2] = new Pokemon(4, Pokemon.Gender.MALE, 5,  Pokemon.Ball.POKE, "None", "Gouldie", 0);

        setUpPokemonButtons();
    }

	// Use this for initialization
	void Start () {

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

        // Set active Pokemon
        setEnemyActivePokemon(opponentParty[0]);
        setPlayerActivePokemon(playerParty[0]);

        setUpPokemonButtons();

        // Set Pokemon sprites
        opponentBase.transform.FindChild("Pokemon").GetComponent<Image>().sprite = enemyActivePokemon.GetFrontSprite();
        playerBase.transform.FindChild("Pokemon").GetComponent<Image>().sprite = playerActivePokemon.GetBackSprite();

        if (trainerBattle) {

        } else {
            StartCoroutine(BattleScreenFade.main.FadeCutout(1.5f));
            StartCoroutine(slidePokemon(opponentBase, false, new Vector2(87,16)));
            yield return StartCoroutine(slidePokemon(playerBase, true, new Vector2(-84,-58)));

            Dialog.DrawDialogBox();
            StartCoroutine(Dialog.DrawTextSilent("A wild " + enemyActivePokemon.getName() + " appeared!"));
            yield return new WaitForSeconds(1.0f);
            Dialog.UndrawDialogBox();

        }

        setSelectedTask(0);

        while (running) {

            runState = true;
            while (runState) {
                if (taskSelected == 0) {
                    OptionBox.SetActive(true);
                    PokemonSelectionBox.SetActive(false);
                }
                else if (taskSelected == 4) {
                    if (trainerBattle) {
                        OptionBox.SetActive(false);
                        Dialog.DrawDialogBox();
                        yield return StartCoroutine(Dialog.DrawTextSilent("No! There's no running from a trainer battle!"));
                        yield return new WaitForSeconds(1.0f);
                        Dialog.UndrawDialogBox();
                        OptionBox.SetActive(true);
                        setSelectedTask(0);
                    } else {
                        OptionBox.SetActive(false);
                        Dialog.DrawDialogBox();
                        yield return StartCoroutine(Dialog.DrawTextSilent("Got away safely!"));
                        yield return new WaitForSeconds(1.0f);
                        Dialog.UndrawDialogBox();
                        setSelectedTask(-1);
                        runState = false;
                        running = false;
                    }
                }
                else if (taskSelected == 2) {
                    OptionBox.SetActive(false);
                    PokemonSelectionBox.SetActive(true);

                    while (taskSelected == 2 && pokemonSelected == 0) {
                        Debug.Log("waiting..");
                        yield return new WaitForSeconds(0.2f);
                    }

                    if (pokemonSelected > 0) {
                        setPlayerActivePokemon(playerParty[pokemonSelected]);
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

    public void setPokemonSelected(int pos) {
        pokemonSelected = pos;
    }

    public bool switchPokemonPlayer(int partyPos) {
        return switchPokemon(true, partyPos, false, false);
    }

    /// Switch Pokemon
    public bool switchPokemon(bool player, int partyPos, bool batonPass) {
        return switchPokemon(player, partyPos, batonPass, false);
    }

    /// Switch Pokemon
    public bool switchPokemon(bool player, int partyPos, bool batonPass, bool forceSwitch) {
        if (partyPos == null || player == null) {
            return false;
        }

        if (player) {
            if (playerParty[partyPos].getStatus() == Pokemon.Status.FAINTED) {
                return false;
            }

            playerActivePokemon = playerParty[partyPos];
            playerActivePokemonMoveset = playerParty[partyPos].getMoveset();
        }

        // Implement forceSwitch later

        // Set PokemonData
        // ...

        return true;
    }

    /// Set the state of Option Box
    public void setSelectedTask(int task) {
        taskSelected = task;
    }

    private void setPlayerActivePokemon(Pokemon pokemon) {
        playerActivePokemon = pokemon;
    }

    private void setEnemyActivePokemon(Pokemon pokemon) {
        enemyActivePokemon = pokemon;
    }

    private void setUpPokemonButtons() {
        if (playerParty[0] != null) {
            setUpPokemonButton(playerParty[0], Pokemon1Button);
        }
        if (playerParty[1] != null) {
            setUpPokemonButton(playerParty[1], Pokemon2Button);
        }
        if (playerParty[2] != null) {
            setUpPokemonButton(playerParty[2], Pokemon3Button);
        }
        if (playerParty[3] != null) {
            setUpPokemonButton(playerParty[3], Pokemon4Button);
        }
        if (playerParty[4] != null) {
            setUpPokemonButton(playerParty[4], Pokemon5Button);
        }
        if (playerParty[5] != null) {
            setUpPokemonButton(playerParty[5], Pokemon6Button);
        }
    }

    private void setUpPokemonButton(Pokemon pokemon, GameObject button) {
        button.SetActive(true);
        button.transform.FindChild("Pokemon Name").GetComponent<Text>().text = pokemon.getName();

        if (pokemon == playerActivePokemon) {
            button.GetComponent<Button>().interactable = false;
        } else {
            button.GetComponent<Button>().interactable = true;
        }
    }

}
