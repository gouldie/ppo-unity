using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour {

    public static Scene main;

    public BattleHandler Battle;

	void Awake() {
        if (main == null) {
            main = this;
        }
    }
}
