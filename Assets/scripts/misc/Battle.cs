using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {

	public bool isAnimating = false;
	public float animSpeed = 1f;
	float t;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void WildBattle(Camera camera) {
		float camHeight = 2f * camera.orthographicSize;
		float camWidth = camHeight * camera.aspect;
		Vector2 battleScreenStartPos = new Vector2(camera.transform.position.x + camWidth/2, camera.transform.position.y);
		Vector2 battleScreenEndPos = new Vector2(camera.transform.position.x, camera.transform.position.y);
	
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (camWidth*2/3, camHeight*2/3);
		StartCoroutine (Animate(battleScreenStartPos, battleScreenEndPos));
	}

	public IEnumerator Animate(Vector2 startPos, Vector2 endPos) {
		isAnimating = true;
		t = 0;
		GetComponent<Canvas> ().enabled = true;


		while (t < 1f) {
			t += Time.deltaTime * animSpeed;
			transform.position = Vector3.Lerp (startPos, endPos, t);
			yield return null;
		}

		isAnimating = false;
		yield return 0;
	}
}
