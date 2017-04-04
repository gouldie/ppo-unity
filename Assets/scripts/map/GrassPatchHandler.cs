//Original Scripts by IIColour (IIColour_Spectrum)

using UnityEngine;
using System.Collections;

public class GrassPatchHandler : MonoBehaviour {
    private GameObject overlay;
//    private AudioSource walkSound;
//    public AudioClip walkClip;

    void Awake() {
        overlay = transform.FindChild("Overlay").gameObject;
//        walkSound = transform.GetComponent<AudioSource>();
    }

    void Start() {
        overlay.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        if (other.tag == "Player") {
            overlay.SetActive(true);


        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log(other);
        if (other.tag == "Player") {
            overlay.SetActive(false);


        }
    }
}