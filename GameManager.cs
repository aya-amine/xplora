
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Array to store question data
    public questiondata[] questions;

    // List to store unanswered questions
    private static List<questiondata> unansweredQuestions;

    // Current question being displayed
    private questiondata CurrentQuestion;

    // Player score
    private int score = 0;

    // UI elements for displaying question and answers
    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text TrueAnswerText;

    [SerializeField]
    private Text FalseAnswerText;

    [SerializeField]
    private Image backgroundImage;

    // Animator for handling animations
    [SerializeField]
    private Animator animator;

    // Time delay between questions
    [SerializeField]
    private float timeBetweenQuestions = 2f;

    // Number of questions to be displayed in each batch
    [SerializeField]
    private int batchSize = 8; // Change to the desired batch size

    // Number of questions to be displayed
    [SerializeField]
    private int numberOfQuestions = 7; // Change to the desired number of questions

    // UI element for displaying the player's score
    [SerializeField]
    private Text scoreText;

    // Variable to track the start index of the current question batch
    private int currentBatchStartIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Check if unansweredQuestions is null or empty
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            // Use the specified number of questions
            unansweredQuestions = questions.Take(numberOfQuestions).ToList<questiondata>();
        }

        // Select questions for the current batch
        SelectQuestionsForCurrentBatch();
    }

    // To make sure the same question doesn't appear twice
    void SelectQuestionsForCurrentBatch()
    {
        // Calculate the end index for the current batch
        int batchEndIndex = currentBatchStartIndex + batchSize;

        // Use questions in the specified batch range
        List<questiondata> batchQuestions = questions.Skip(currentBatchStartIndex).Take(batchSize).ToList();

        // Shuffle the questions within the batch
        ShuffleQuestions(batchQuestions);

        // Set unansweredQuestions to the shuffled batch
        unansweredQuestions = batchQuestions;

        // Move to the next batch for the next time
        currentBatchStartIndex += batchSize;

        // If we've reached the end of questions, reset the batch
        if (currentBatchStartIndex >= questions.Length)
        {
            currentBatchStartIndex = 0;
        }
    }

    // Coroutine to load the exit scene after a delay
    IEnumerator DelayedLoadExitScene()
    {
        // Wait for a short delay to ensure PlayerPrefs data is saved
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed

        // Load the exitScene after the delay
        SceneManager.LoadScene("exitScene");
    }

    // Coroutine to transition to the next question
    IEnumerator TransitionToNextQuestion()
    {
        // Remove the current question from the unansweredQuestions list
        unansweredQuestions.Remove(CurrentQuestion);

        // Check if there are more unanswered questions
        if (unansweredQuestions.Count > 0)
        {
            // Wait for a short delay before loading the next question
            yield return new WaitForSeconds(timeBetweenQuestions);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Save the player's score to PlayerPrefs before loading the exit scene
            PlayerPrefs.SetInt("PlayerScore", score);
            PlayerPrefs.Save();

            // Load the exitScene with a delay
            StartCoroutine(DelayedLoadExitScene());

            // Select questions for the next batch
            SelectQuestionsForCurrentBatch();
        }
    }

    // Method to handle when the player selects True
    public void UserSelectTrue()
    {
        // Trigger the "true" animation
        animator.SetTrigger("true");

        // Check if the selected answer is correct
        if (CurrentQuestion.isTrue)
        {
            // Log correct answer and increment the score
            Debug.Log(" CORRECT! ");
            score += 1;
        }
        else
        {
            // Log incorrect answer
            Debug.Log(" WRONG! ");
        }

        // Start the transition to the next question
        StartCoroutine(TransitionToNextQuestion());
    }

    // Method to handle when the player selects False
    public void UserSelectFalse()
    {
        // Trigger the "false" animation
        animator.SetTrigger("false");

        // Check if the selected answer is correct
        if (!CurrentQuestion.isTrue)
        {
            // Log correct answer and increment the score
            Debug.Log(" CORRECT! ");
            score += 1;
        }
        else
        {
            // Log incorrect answer
            Debug.Log(" WRONG! ");
        }

        // Start the transition to the next question
        StartCoroutine(TransitionToNextQuestion());
    }

    // Method to shuffle the questions within a batch
    private void ShuffleQuestions(List<questiondata> questionList)
    {
        System.Random random = new System.Random();
        int n = questionList.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            var value = questionList[k];
            questionList[k] = questionList[n];
            questionList[n] = value;
        }
    }
}

