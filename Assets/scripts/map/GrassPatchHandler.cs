//Original Scripts by IIColour (IIColour_Spectrum)

using UnityEngine;
using System.Collections;

public class GrassPatchHandler : MonoBehaviour {
    private GameObject overlay;
//    private AudioSource walkSound;
//    public AudioClip walkClip;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool animate = false;
    public float animDuration = 0.5f;
    public float animSpeed = 20f;
    public float increment = 0;
    private float step = 0;

    void Awake() {
        overlay = transform.FindChild("Overlay").gameObject;
        startPos = overlay.transform.position;
        endPos = new Vector3(overlay.transform.position.x, overlay.transform.position.y - 0.3f, overlay.transform.position.z);
//        walkSound = transform.GetComponent<AudioSource>();
    }

    void Start() {
        overlay.SetActive(false);
    }

    void Update() {
        if (animate) {
            step += animSpeed * Time.deltaTime;
            overlay.transform.position = Vector3.MoveTowards(startPos, endPos, step);
            if (overlay.transform.position.y <= endPos.y) {
                step = 0;
                animate = false;
                overlay.SetActive(false);
                overlay.transform.position = startPos;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            overlay.SetActive(true);
            animate = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            overlay.SetActive(false);

            overlay.transform.position = startPos;
            step = 0;
        }
    }
}