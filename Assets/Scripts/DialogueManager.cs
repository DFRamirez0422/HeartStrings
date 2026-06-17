using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        [Header("Teaching Dialogue Before Question")]
        [TextArea(2, 5)]
        public string[] teachingLines;

        [Header("Question")]
        [TextArea(2, 5)]
        public string questionText;

        public string answerA;
        public string answerB;
        public string answerC;

        [Tooltip("Use 0 for A, 1 for B, 2 for C")]
        public int correctAnswerIndex;

        [Header("Feedback")]
        [TextArea(2, 5)]
        public string[] correctFeedbackLines;

        [TextArea(2, 5)]
        public string[] incorrectFeedbackLines;
    }

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text dialogueText;

    [Header("Question UI")]
    [SerializeField] private GameObject questionBox;
    [SerializeField] private Text questionText;

    [Header("Answer Buttons")]
    [SerializeField] private GameObject[] answerButtons;
    [SerializeField] private Text[] answerButtonTexts;

    [Header("Next Button")]
    [SerializeField] private GameObject nextButton;

    [Header("Questions")]
    [SerializeField] private Question[] questions;

    [Header("Dialogue Settings")]
    [SerializeField] private float typeSpeed = 0.04f;

    private int currentQuestionIndex = 0;
    private int currentLineIndex = 0;

    private string[] currentDialogueLines;

    private bool isTyping = false;
    private bool dialogueActive = false;
    private bool showingFeedback = false;
    private bool lastAnswerWasCorrect = false;

    private Coroutine typingCoroutine;

    private void Start()
    {
        if (nextButton != null)
        {
            nextButton.SetActive(false);
        }

        HideQuestionUI();

        currentQuestionIndex = 0;
        StartTeachingForCurrentQuestion();
    }

    private void Update()
    {
        if (!dialogueActive)
        {
            return;
        }

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            ContinueDialogue();
        }
    }

    private void StartTeachingForCurrentQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            EndAllQuestions();
            return;
        }

        showingFeedback = false;
        StartDialogue(questions[currentQuestionIndex].teachingLines);
    }

    private void StartDialogue(string[] lines)
    {
        currentDialogueLines = lines;
        currentLineIndex = 0;
        dialogueActive = true;

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
        dialogueActive = false;
        dialogueBox.SetActive(false);

        if (showingFeedback)
        {
            if (lastAnswerWasCorrect)
            {
                currentQuestionIndex++;
                StartTeachingForCurrentQuestion();
            }
            else
            {
                ShowQuestionUI();
            }
        }
        else
        {
            ShowQuestionUI();
        }
    }

    private void ShowQuestionUI()
    {
        Question currentQuestion = questions[currentQuestionIndex];

        questionBox.SetActive(true);
        questionText.text = currentQuestion.questionText;

        answerButtons[0].SetActive(true);
        answerButtons[1].SetActive(true);
        answerButtons[2].SetActive(true);

        answerButtonTexts[0].text = currentQuestion.answerA;
        answerButtonTexts[1].text = currentQuestion.answerB;
        answerButtonTexts[2].text = currentQuestion.answerC;
    }

    private void HideQuestionUI()
    {
        if (questionBox != null)
        {
            questionBox.SetActive(false);
        }

        SetObjectsActive(answerButtons, false);
    }

    public void SelectAnswer(int answerIndex)
    {
        Question currentQuestion = questions[currentQuestionIndex];

        HideQuestionUI();

        lastAnswerWasCorrect = answerIndex == currentQuestion.correctAnswerIndex;
        showingFeedback = true;

        if (lastAnswerWasCorrect)
        {
            StartDialogue(currentQuestion.correctFeedbackLines);
        }
        else
        {
            StartDialogue(currentQuestion.incorrectFeedbackLines);
        }
    }

    private void EndAllQuestions()
    {
        dialogueBox.SetActive(false);
        HideQuestionUI();

        if (nextButton != null)
        {
            nextButton.SetActive(true);
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