using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxHandler : MonoBehaviour {
    private Image dialogBox;
    private Text dialogBoxText;
//    private Text dialogBoxShadow;
//    private Image dialogBoxBorder;

    private float charPerSec = 60f;
    public float scrollSpeed = 0.1f;

    public int chosenIndex;

    public bool hideDialogOnStart = true;
    public int defaultDialogLines = 2;

    void Awake() {
        Transform dialogBoxTrn = transform.FindChild("DialogBox");
        dialogBox = dialogBoxTrn.GetComponent<Image>();
        dialogBoxText = dialogBoxTrn.FindChild("BoxText").GetComponent<Text>();

        defaultDialogLines = Mathf.RoundToInt((dialogBox.rectTransform.sizeDelta.y - 16f) / 14f);

    }

    void Start() {
        if (hideDialogOnStart) {
            dialogBox.gameObject.SetActive(false);
        }
    }

    public IEnumerator DrawText(string text) {
        yield return StartCoroutine(DrawText(text, 1f / charPerSec, false));
    }

    public IEnumerator DrawText(string text, float secPerChar) {
        yield return StartCoroutine(DrawText(text, secPerChar, false));
    }

    public IEnumerator DrawTextSilent(string text) {
        yield return StartCoroutine(DrawText(text, 1f / charPerSec, true));
    }

    public IEnumerator DrawTextInstant(string text) {
        yield return StartCoroutine(DrawText(text, 0, false));
    }

    public IEnumerator DrawText(string text, float secPerChar, bool silent) {
        string[] words = text.Split(new char[] {' '});

        // Implement sound later
//        if (!silent)
//        {
//            SfxHandler.Play(selectClip);
//        }
        for (int i = 0; i < words.Length; i++) {
            if (secPerChar > 0) {
                yield return StartCoroutine(DrawWord(words[i], secPerChar));
            }
            else {
                StartCoroutine(DrawWord(words[i], secPerChar));
            }
        }
    }

    private IEnumerator DrawWord(string word, float secPerChar) {
        yield return StartCoroutine(DrawWord(word, false, false, false, secPerChar));
    }

    private IEnumerator DrawWord(string word, bool large, bool bold, bool italic, float secPerChar) {
        char[] chars = word.ToCharArray();
        float startTime = Time.time;
        if (chars.Length > 0) {
            //ensure no blank words get processed
            if (chars[0] == '\\') {
                //Apply Operator
                switch (chars[1]) {
                    case ('p'): //Player
                        if (secPerChar > 0) {
                            yield return
                            StartCoroutine(DrawWord("Player Name", large, bold, italic, secPerChar))
                            ;
                        }
                        else
                        {
                            StartCoroutine(DrawWord("Player Name", large, bold, italic, secPerChar));
                        }
                        break;
                    case ('l'): //Large
                        large = true;
                        break;
                    case ('b'): //Bold
                        bold = true;
                        break;
                    case ('i'): //Italic
                        italic = true;
                        break;
                    case ('n'): //New Line
                        dialogBoxText.text += "\n";
                        break;
                }
                if (chars.Length > 2) {
                    //Run this function for the rest of the word
                    string remainingWord = "";
                    for (int i = 2; i < chars.Length; i++) {
                        remainingWord += chars[i].ToString();
                    }
                    yield return StartCoroutine(DrawWord(remainingWord, large, bold, italic, secPerChar));
                }
            }
            else
            {
                //Draw Word
                string currentText = dialogBoxText.text;

                for (int i = 0; i <= chars.Length; i++) {
                    string added = "";

                    //apply open tags
                    added += (large) ? "<size=26>" : "";
                    added += (bold) ? "<b>" : "";
                    added += (italic) ? "<i>" : "";

                    //apply displayed text
                    for (int i2 = 0; i2 < i; i2++) {
                        added += chars[i2].ToString();
                    }

                    //apply hidden text
                    added += "<color=#0000>";
                    for (int i2 = i; i2 < chars.Length; i2++) {
                        added += chars[i2].ToString();
                    }
                    added += "</color>";

                    //apply close tags
                    added += (italic) ? "</i>" : "";
                    added += (bold) ? "</b>" : "";
                    added += (large) ? "</size>" : "";

                    dialogBoxText.text = currentText + added;
//                    dialogBoxTextShadow.text = dialogBoxText.text;

                    while (Time.time < startTime + (secPerChar * (i + 1))) {
                        yield return null;
                    }
                }

                //add a space after every word
                dialogBoxText.text += " ";
//                dialogBoxTextShadow.text = dialogBoxText.text;
                while (Time.time < startTime + (secPerChar)) {
                    yield return null;
                }
            }
        }
    }

    public void DrawDialogBox() {
        StartCoroutine(DrawDialogBox(defaultDialogLines, new Color(1, 1, 1, 1), false));
    }

    public void DrawDialogBox(int lines) {
        StartCoroutine(DrawDialogBox(lines, new Color(1, 1, 1, 1), false));
    }

    public void DrawSignBox(Color tint) {
        StartCoroutine(DrawDialogBox(defaultDialogLines, tint, true));
    }

    private IEnumerator DrawDialogBox(int lines, Color tint, bool sign) {
        dialogBox.gameObject.SetActive(true);
//        dialogBoxBorder.sprite = (sign)
//        ? null
//        : Resources.Load<Sprite>("Frame/dialog" + PlayerPrefs.GetInt("frameStyle"));
        dialogBox.sprite = (sign) ? Resources.Load<Sprite>("Frame/signBG") : Resources.Load<Sprite>("Frame/dialogBG");
        dialogBox.color = tint;
        dialogBoxText.text = "";
        dialogBoxText.color = (sign) ? new Color(1f, 1f, 1f, 1f) : new Color(0.0625f, 0.0625f, 0.0625f, 1f);
//        dialogBoxTextShadow.text = dialogBoxText.text;

        dialogBox.rectTransform.sizeDelta = new Vector2(dialogBox.rectTransform.sizeDelta.x,
                Mathf.Round((float) lines * 14f) + 16f);
//        dialogBoxBorder.rectTransform.sizeDelta = new Vector2(dialogBox.rectTransform.sizeDelta.x,
//                dialogBox.rectTransform.sizeDelta.y);
//        dialogBoxText.rectTransform.localPosition = new Vector3(dialogBoxText.rectTransform.localPosition.x,
//                -37f + Mathf.Round((float) lines * 14f), 0);
//        dialogBoxTextShadow.rectTransform.localPosition = new Vector3(
//                dialogBoxTextShadow.rectTransform.localPosition.x, dialogBoxText.rectTransform.localPosition.y - 1f, 0);

        if (sign) {
            float increment = 0f;
            while (increment < 1) {
                increment += (1f / 0.2f) * Time.deltaTime;
                if (increment > 1) {
                    increment = 1;
                }

                dialogBox.rectTransform.localPosition = new Vector2(dialogBox.rectTransform.localPosition.x,
                        -dialogBox.rectTransform.sizeDelta.y + (dialogBox.rectTransform.sizeDelta.y * increment));
                yield return null;
            }
        }
    }

    private bool UpdateChosenIndex(int newIndex, int choicesLength, string[] flavourText)
    {
        //Check for an invalid new index
        if (newIndex < 0 || newIndex >= choicesLength)
        {
            return false;
        }
        //Even if new index is the same as old, set the graphics in case of needing to override modified graphics.
//        choiceBoxSelect.rectTransform.localPosition = new Vector3(8, 9f + (14f * newIndex), 0);
        if (flavourText != null)
        {
            DrawDialogBox();
            StartCoroutine(DrawText(flavourText[flavourText.Length - 1 - newIndex], 0));
        }
        //If chosen index is the same as before, do not play a sound effect, then return false
        if (chosenIndex == newIndex)
        {
            return false;
        }
        chosenIndex = newIndex;
//        SfxHandler.Play(selectClip);
        return true;
    }

    public void UndrawDialogBox()
    {
        dialogBox.gameObject.SetActive(false);
    }

    public IEnumerator UndrawSignBox()
    {
        float increment = 0f;
        while (increment < 1)
        {
            increment += (1f / 0.2f) * Time.deltaTime;
            if (increment > 1)
            {
                increment = 1;
            }

            dialogBox.rectTransform.localPosition = new Vector2(dialogBox.rectTransform.localPosition.x,
                    -dialogBox.rectTransform.sizeDelta.y * increment);
            yield return null;
        }
        dialogBox.gameObject.SetActive(false);
    }
}