using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private int lettersPerSecond = 24;

    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;

    public static DialogueManager Instance { get; private set; }

    private Dialogue dialogue;
    private int currentLine;
    private bool isTyping;
    private Coroutine typingCo;

    private void Awake()
    {
        Instance = this;
        if (dialogueBox) dialogueBox.SetActive(false);
    }

    public IEnumerator ShowDialogue(Dialogue d)
    {
        // Basic guards to prevent the TMP null exceptions
        if (dialogueText == null || dialogueBox == null)
        {
            Debug.LogError("DialogueManager: dialogueText or dialogueBox is not assigned.");
            yield break;
        }
        if (lettersPerSecond <= 0) lettersPerSecond = 24;

        yield return null; // let one frame pass if you like

        dialogue = d;
        currentLine = 0;

        OnShowDialogue?.Invoke();
        dialogueBox.SetActive(true);

        if (typingCo != null) StopCoroutine(typingCo);
        typingCo = StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (dialogue == null || isTyping) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentLine++;
            if (currentLine < dialogue.Lines.Count)
            {
                if (typingCo != null) StopCoroutine(typingCo);
                typingCo = StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                Close();
            }
        }
    }

    private void Close()
    {
        dialogue = null;
        currentLine = 0;
        isTyping = false;
        if (dialogueBox) dialogueBox.SetActive(false);
        OnCloseDialogue?.Invoke();
    }

    private IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = string.Empty;

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
    }
}
