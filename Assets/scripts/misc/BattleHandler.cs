using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    public bool run = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setRunning() {
        run = true;
	}

    public IEnumerator control() {
        while (!run) {
            yield return null;
        }

        Scene.main.Battle.gameObject.SetActive(false);
        run = false;
    }
}
