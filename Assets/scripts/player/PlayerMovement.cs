using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement player;

    public GameObject busyWith;

    public Camera mainCamera;
    public Vector3 mainCameraDefaultPos;
    public float mainCameraDefaultSize;

    private Transform pawn;
    private SpriteRenderer pawnSprite;

    public MapSettings accessedMapSettings;

    private string animationName;
    public Sprite[] walkSpriteSheet;
    public Sprite[] runSpriteSheet;
    private Sprite[] spriteSheet;

    public Transform hitBox;

    // currentmap & destinationmap

    public GameObject busy;

    public bool moving = false;
    public bool still = true;
    public bool running = false;
    public float walkSpeed = 0.3f;
    public float runSpeed = 0.15f;
    public float speed;
    public int direction = 2;
    public float increment = 1f;
    public int walkFPS = 7;
    public int runFPS = 12;

    private int frame;
    private int frames;
    private int framesPerSec;
    private float secPerFrame;
    private bool animPause;
    private bool overrideAnimPause;

    public bool canInput = true;


    private int lastDirectionPressed = 0;
    private float directionChangeInputDelay = 0.08f;

    void Awake() {
        player = this;

        canInput = true;
        speed = walkSpeed;

        mainCamera = transform.FindChild("Camera").GetComponent<Camera>();
        mainCameraDefaultPos = mainCamera.transform.localPosition;
        mainCameraDefaultSize = mainCamera.orthographicSize;
        
        float width = Screen.width / 120f;

        mainCamera.orthographicSize = width;

        pawn = transform.FindChild("Pawn");
        pawnSprite = pawn.GetComponent<SpriteRenderer>();

        hitBox = transform.FindChild("Player_Transparent");
    }

	// Use this for initialization
	void Start () {
		updateAnimation("walk", walkFPS);
        StartCoroutine("animateSprite");
        animPause = true;

        updateDirection(direction);

        StartCoroutine(control());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                lastDirectionPressed = 1;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                lastDirectionPressed = 3;
            }
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                lastDirectionPressed = 0;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                lastDirectionPressed = 2;
            }
        }
	}

    public void updateAnimation(string newAnimationName, int fps) {
        if (animationName != newAnimationName) {
            animationName = newAnimationName;

            if (newAnimationName == "walk") spriteSheet = walkSpriteSheet;
            if (newAnimationName == "run") spriteSheet = runSpriteSheet;

            framesPerSec = fps;
            secPerFrame = 1f / (float) framesPerSec;
            frames = Mathf.RoundToInt((float) spriteSheet.Length / 4f);
            if (frames >= frames) {
                frame = 0;
            }
        }
    }

    private IEnumerator animateSprite() {
        frame = 0;
        frames = 4;
        framesPerSec = walkFPS;
        secPerFrame = 1f / (float) framesPerSec;

        while (true) {
            for (int i = 0; i < 4; i++) {
                if (animPause && frame % 2 != 0 && !overrideAnimPause) {
                    frame -= 1;
                }
                pawnSprite.sprite = spriteSheet[direction * frames + frame];
                yield return new WaitForSeconds(secPerFrame / 4f);
            }
            if (!animPause || overrideAnimPause) {
                frame += 1;
                if (frame >= frames) {
                    frame = 0;
                }
            }
        }
    }

    public void updateDirection(int dir) {
        direction = dir;
        pawnSprite.sprite = spriteSheet[direction * frames + frame];
    }

    private IEnumerator control() {
        bool still;
        while (true) {
            still = true;

            if (canInput) {
                if (Input.GetButton("Run")) {
                    running = true;
                    if (moving) {
                        updateAnimation("run", runFPS);
                    } else {
                        updateAnimation("walk", walkFPS);
                    }
                    speed = runSpeed;
                } else {
                    running = false;
                    updateAnimation("walk", walkFPS);
                    speed = walkSpeed;
                }

                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                    if (lastDirectionPressed != direction && isDirectionKeyHeld(lastDirectionPressed)) {
                        updateDirection(lastDirectionPressed);
                        if (!moving) {
                            // Turning
                            yield return new WaitForSeconds(directionChangeInputDelay);
                        }
                    } else {
                        if (!isDirectionKeyHeld(direction)) {
                            int directionCheck = (direction + 2 > 3) ? direction - 2 : direction + 2;
                            if (isDirectionKeyHeld(directionCheck)) {
                                updateDirection(directionCheck);
                                if (!moving) {
                                    yield return new WaitForSeconds(directionChangeInputDelay);
                                }
                            } else {
                                directionCheck = (direction + 1 > 3) ? direction - 3 : direction + 1;
                                if (isDirectionKeyHeld(directionCheck)) {
                                    updateDirection(directionCheck);
                                    if (!moving) {
                                        yield return new WaitForSeconds(directionChangeInputDelay);
                                    }
                                } else {
                                    directionCheck = (direction - 1 < 0) ? direction + 3 : direction - 1;
                                    if (isDirectionKeyHeld(directionCheck)) {
                                        updateDirection(directionCheck);
                                        if (!moving) {
                                            yield return new WaitForSeconds(directionChangeInputDelay);
                                        }
                                    }
                                }
                            }
                        } else {
                            moving = true;
                        }
                    }
                    if (moving) {
                        still = false;
                        yield return StartCoroutine(moveForward());
                    }
                }
            }

            if (still) {
                animPause = true;
                moving = false;
            }
            yield return null;
        }
    }

    private bool isDirectionKeyHeld(int directionCheck) {
        bool directionHeld = false;

        if (directionCheck == 0 && Input.GetAxisRaw("Vertical") > 0) {
            directionHeld = true;
        }
        if (directionCheck == 1 && Input.GetAxisRaw("Horizontal") > 0) {
            directionHeld = true;
        }
        if (directionCheck == 2 && Input.GetAxisRaw("Vertical") < 0) {
            directionHeld = true;
        }
        if (directionCheck == 3 && Input.GetAxisRaw("Horizontal") < 0) {
            directionHeld = true;
        }
        return directionHeld;
    }

    private IEnumerator moveForward() {
        Vector2 movement = getForwardVector();

        bool ableToMove = false;
        Collider2D blockCollider = null;

        if (movement != Vector2.zero) {
            Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(playerPos + movement,0.4f);

            for (int i = 0; i < hitColliders.Length; i++) {
                // add more variations later, e.g. water
                if (hitColliders[i].gameObject.tag == "Block") {
                    blockCollider = hitColliders[i];
                }
            }

            if (blockCollider == null) {
                ableToMove = true;
                yield return StartCoroutine(move(movement));
            }
        }

        if (!ableToMove)
        {
            moving = false;
            animPause = true;
        }
    }

    public Vector2 getForwardVector() {
        return getForwardVector(direction);
    }

    public Vector2 getForwardVector(int direction) {
        Vector2 movement = getForwardVectorRaw(direction);

        // todo: check for map collision

        return movement;
    }

    public Vector2 getForwardVectorRaw() {
        return getForwardVectorRaw(direction);
    }

    public Vector2 getForwardVectorRaw(int direction) {
        Vector2 forwardVector = new Vector2(0, 0);

        if (direction == 0) {
            forwardVector = new Vector2(0, 1);
        }
        else if (direction == 1) {
            forwardVector = new Vector2(1, 0);
        }
        else if (direction == 2) {
            forwardVector = new Vector2(0, -1);
        }
        else if (direction == 3) {
            forwardVector = new Vector2(-1, 0);
        }
        return forwardVector;
    }

    private IEnumerator move(Vector2 movement) {
        if (movement != Vector2.zero) {
            Vector3 startPos = new Vector3(transform.position.x, transform.position.y, 0);
            Vector3 endPos = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, 0);

            moving = true;
            increment = 0;
            animPause = false;


            while (increment < 1f) {
                increment += Time.deltaTime * (1f / speed);
                transform.position = Vector3.Lerp(startPos, endPos, increment);
                yield return null;
            }
        }
    }

    public bool setCheckBusyWith(GameObject caller) {
        if (PlayerMovement.player.busyWith == null) {
            PlayerMovement.player.busyWith = caller;
        }

        if (PlayerMovement.player.busyWith == caller) {
            pauseInput();
            return true;
        }
        return false;
    }

    public IEnumerator checkBusinessBeforeUnpause(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if (PlayerMovement.player.busyWith == null) {
            unpauseInput();
        }
    }

    public void pauseInput() {
        canInput = false;

        if (animationName == "run") {
            updateAnimation("walk", walkFPS);
        }
        running = false;
    }

    public void pauseInput(float secondsToWait) {
        pauseInput();
        StartCoroutine(checkBusinessBeforeUnpause(secondsToWait));
    }

    public void unpauseInput() {
        canInput = true;
    }

    public void unsetCheckBusyWith (GameObject caller) {
        if (PlayerMovement.player.busyWith == caller) {
            PlayerMovement.player.busyWith = null;
        }
        StartCoroutine(checkBusinessBeforeUnpause(0.1f));
    }

    public IEnumerator wildEncounter(EncounterTypes type) {
        if (setCheckBusyWith(Scene.main.Battle.gameObject)) {

            Scene.main.Battle.gameObject.SetActive(true);

            StartCoroutine(Scene.main.Battle.control(accessedMapSettings.GetRandomEncounter(type)));

            while (Scene.main.Battle.gameObject.activeSelf) {
                yield return null;
            }

            unsetCheckBusyWith(Scene.main.Battle.gameObject);
            yield return null;
        }
    }
}
