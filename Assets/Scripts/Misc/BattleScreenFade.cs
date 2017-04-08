using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScreenFade : MonoBehaviour {

    public static BattleScreenFade main;
    public static float speed = 1.2f;

    private Image image;

    public Sprite fadeSprite;
    public Material fadeMaterial;

	void Awake() {
        if (main == null) {
            main = this;
        }

        image = this.GetComponent<Image>();
    }
	
	public IEnumerator FadeCutout(float speed) {
        main.gameObject.SetActive(true);
        image.sprite = fadeSprite;
        image.material = fadeMaterial;

        float increment = 0f;

        while (increment < 1) {
            increment += (1 / speed) * Time.deltaTime;
            if (increment > 1) {
                increment = 1;
            }

            float alpha = increment;

            image.material.SetFloat("_Cutoff", alpha);
            yield return null;
        }
        main.gameObject.SetActive(false);
    }
}
