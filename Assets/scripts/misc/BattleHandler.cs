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

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

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

        if (trainerBattle) {

        } else {
            StartCoroutine(BattleScreenFade.main.FadeCutout(1.5f));
            StartCoroutine(slidePokemon(opponentBase, false, new Vector2(87,16)));
            yield return StartCoroutine(slidePokemon(playerBase, true, new Vector2(-84,-58)));
        }


        yield return null;
    }

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
}
