using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text dialogueText;

    [Header("Continue Button")]
    [SerializeField] private GameObject continueButton;

    [Header("Dialogue Settings")]
    [TextArea(2, 5)]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private float typeSpeed = 0.04f;

    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool dialogueFinished = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        dialogueBox.SetActive(true);

        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }

        StartDialogue();
    }

    private void Update()
    {
        if (dialogueFinished)
        {
            return;
        }

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            ContinueDialogue();
        }
    }

    private void StartDialogue()
    {
        currentLineIndex = 0;
        ShowLine();
    }

    private void ContinueDialogue()
    {
        if (isTyping)
        {
            // If player presses while text is typing, instantly finish the line
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[currentLineIndex];
            isTyping = false;
        }
        else
        {
            currentLineIndex++;

            if (currentLineIndex < dialogueLines.Length)
            {
                ShowLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void ShowLine()
    {
        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    private void EndDialogue()
    {
        dialogueFinished = true;
        dialogueBox.SetActive(false);

        if (continueButton != null)
        {
            continueButton.SetActive(true);
        }
    }
}