using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text dialogueText;

    [Header("Answer Buttons")]
    [SerializeField] private GameObject[] answerButtons;

    [Header("Next Button")]
    [SerializeField] private GameObject nextButton;

    [Header("Intro Dialogue")]
    [TextArea(2, 5)]
    [SerializeField] private string[] introDialogueLines;

    [Header("Feedback Dialogue")]
    [TextArea(2, 5)]
    [SerializeField] private string[] correctDialogueLines;

    [TextArea(2, 5)]
    [SerializeField] private string[] incorrectDialogueLines;

    [Header("Dialogue Settings")]
    [SerializeField] private float typeSpeed = 0.04f;

    private string[] currentDialogueLines;
    private int currentLineIndex = 0;

    private bool isTyping = false;
    private bool dialogueFinished = false;
    private bool showingFeedback = false;
    private bool lastAnswerWasCorrect = false;

    private Coroutine typingCoroutine;

    private void Start()
    {
        dialogueBox.SetActive(true);

        SetObjectsActive(answerButtons, false);

        if (nextButton != null)
        {
            nextButton.SetActive(false);
        }

        StartDialogue(introDialogueLines, false);
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

    private void StartDialogue(string[] lines, bool isFeedback)
    {
        currentDialogueLines = lines;
        currentLineIndex = 0;
        dialogueFinished = false;
        showingFeedback = isFeedback;

        dialogueBox.SetActive(true);
        ShowLine();
    }

    private void ContinueDialogue()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogueLines[currentLineIndex];
            isTyping = false;
        }
        else
        {
            currentLineIndex++;

            if (currentLineIndex < currentDialogueLines.Length)
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
        typingCoroutine = StartCoroutine(TypeLine(currentDialogueLines[currentLineIndex]));
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

        if (!showingFeedback)
        {
            // Intro dialogue finished, show answer buttons
            SetObjectsActive(answerButtons, true);
            return;
        }

        if (lastAnswerWasCorrect)
        {
            // Correct answer finished, show next button
            if (nextButton != null)
            {
                nextButton.SetActive(true);
            }
        }
        else
        {
            // Wrong answer finished, let player try again
            SetObjectsActive(answerButtons, true);
        }
    }

    public void SelectAnswer(bool isCorrect)
    {
        SetObjectsActive(answerButtons, false);

        lastAnswerWasCorrect = isCorrect;

        if (isCorrect)
        {
            StartDialogue(correctDialogueLines, true);
        }
        else
        {
            StartDialogue(incorrectDialogueLines, true);
        }
    }

    private void SetObjectsActive(GameObject[] objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(active);
            }
        }
    }
}