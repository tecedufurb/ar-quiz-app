using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for handle the quiz behaviors.
/// </summary>
public class Quiz : MonoBehaviour {

    [SerializeField] private GameScreenHandle GameScreenBehavior;
    [SerializeField] private GameObject QuestionPrefab;
    [SerializeField] private GameObject ImageTarget;

    private List<Question> mQuestions;
    private int mQuestionAmount;
    private int mRightQuestionsCount = 0;
    private int mWrongQuestionsCount = 0;
    private int mScore;

    /// <summary>
    /// mScore variable property.
    /// </summary>
    public int Score {
        get {
            return mScore;
        }
        set {
            mScore = value;
        }
    }
    
    void Start() {
        mQuestions = QuestionSingleTon.Instance.Questions;
        mQuestionAmount = mQuestions.Count - 1;

        InstantiateQuestion();
        ShowQuestion();
    }

    /// <summary>
    /// Verifies the alternative button that was clicked.
    /// </summary>
    /// <param name="alternative"></param>
    public void CheckAlternative(string alternative) {
        switch (alternative) {
            case "A": CheckAnswer(GameScreenBehavior.m_AlternativeAText.text);
                break;
            case "B": CheckAnswer(GameScreenBehavior.m_AlternativeBText.text);
                break;
            case "C": CheckAnswer(GameScreenBehavior.m_AlternativeCText.text);
                break;
            case "D": CheckAnswer(GameScreenBehavior.m_AlternativeDText.text);
                break;
        }
    }

    /// <summary>
    /// Check if the answer is right or not.
    /// </summary>
    /// <param name="answer"></param>
    private void CheckAnswer(string answer) {
        if (answer == mQuestions[mQuestionAmount].m_RightAlternative)
            RightAnswer();
        else
            WrongAnswer();

        if(mQuestionAmount >= 0)
            ShowQuestion();

        GameScreenBehavior.ShowQuestionsScore(mRightQuestionsCount, mWrongQuestionsCount);
    }

    /// <summary>
    /// Randomize a question and its alternatives.
    /// </summary>
    private void RandomizeQuestion() {
        //Randomize the questions of mQuestions list
        for (int i = 0; i < mQuestions.Count; i++) {
            Question temp = mQuestions[i];
            int randomIndex = Random.Range(i, mQuestions.Count);
            mQuestions[i] = mQuestions[randomIndex];
            mQuestions[randomIndex] = temp;
        }

        //Randomize the alternatives of the last question of mQuestions list.
        string[] alternatives = new string[4];
        alternatives[0] = mQuestions[mQuestionAmount].m_AnswerA;
        alternatives[1] = mQuestions[mQuestionAmount].m_AnswerB;
        alternatives[2] = mQuestions[mQuestionAmount].m_AnswerC;
        alternatives[3] = mQuestions[mQuestionAmount].m_AnswerD;

        for (int i = 0; i < alternatives.Length; i++) {
            string temp = alternatives[i];
            int randomIndex = Random.Range(i, alternatives.Length);
            alternatives[i] = alternatives[randomIndex];
            alternatives[randomIndex] = temp;
        }

        mQuestions[mQuestionAmount].m_AnswerA = alternatives[0];
        mQuestions[mQuestionAmount].m_AnswerB = alternatives[1];
        mQuestions[mQuestionAmount].m_AnswerC = alternatives[2];
        mQuestions[mQuestionAmount].m_AnswerD = alternatives[3];
    }

    /// <summary>
    /// Show the last question of mQuestions list and its alternatives.
    /// </summary>
    public void ShowQuestion() {
        RandomizeQuestion();
        
        GameScreenBehavior.m_QuestionText.text = mQuestions[mQuestionAmount].m_Question;
        GameScreenBehavior.m_AlternativeAText.text = mQuestions[mQuestionAmount].m_AnswerA;
        GameScreenBehavior.m_AlternativeBText.text = mQuestions[mQuestionAmount].m_AnswerB;
        GameScreenBehavior.m_AlternativeCText.text = mQuestions[mQuestionAmount].m_AnswerC;
        GameScreenBehavior.m_AlternativeDText.text = mQuestions[mQuestionAmount].m_AnswerD;
    }
    
    /// <summary>
    /// Remove the question from mQuestions list, instantiate a new Question object
    /// and increment the player score.
    /// </summary>
    private void RightAnswer() {
        GameScreenBehavior.EnableQuestionPanel(false);

        mQuestions.Remove(mQuestions[mQuestionAmount]);
        mQuestionAmount--;
        mRightQuestionsCount++;

        IncrementScore();

        GameScreenBehavior.ShowRightAnswerMessage(mQuestionAmount + 1);
        GameScreenBehavior.ShowScore(Score);

        Destroy(GameObject.Find("Question(Clone)"));

        if (mQuestions.Count > 0) {
            InstantiateQuestion();
        } else {
            GameScreenBehavior.EnableMainPanel(false);
            GameScreenBehavior.EnableFinalPanel(true);
        }
    }

    /// <summary>
    /// Decrement the player score.
    /// </summary>
    private void WrongAnswer() {
        mWrongQuestionsCount++;

        if (Score > 0)
            DecrementScore();
        
        GameScreenBehavior.ShowWrongAnswerMessage();
        GameScreenBehavior.ShowScore(Score);
    }

    /// <summary>
    /// Increment the player score based on the difficulty level.
    /// </summary>
    private void IncrementScore() {
        int score = 0;
        switch (QuestionSingleTon.Instance.Difficulty) {
            case Difficulty.EASY: score = 10;
                break;
            case Difficulty.NORMAL: score = 30;
                break;
            case Difficulty.HARD: score = 50;
                break;
        }
        Score += score;
        GameScreenBehavior.ShowAddScoreAnimation(score);
    }

    /// <summary>
    /// Decrement the player score based on the difficulty level.
    /// </summary>
    private void DecrementScore() {
        int score = 0;
        switch (QuestionSingleTon.Instance.Difficulty) {
            case Difficulty.EASY: score = 2;
                break;
            case Difficulty.NORMAL: score = 5;
                break;
            case Difficulty.HARD: score = 10;
                break;
        }
        Score -= score;
        GameScreenBehavior.ShowSubScoreAnimation(score);
    }
    
    /// <summary>
    /// Instantiate a new Question object in a random position.
    /// </summary>
    private void InstantiateQuestion() {
        float wall1 = GameObject.Find("Wall1").transform.position.x;
        float wall2 = GameObject.Find("Wall2").transform.position.x;
        float wall3 = GameObject.Find("Wall3").transform.position.z;
        float wall4 = GameObject.Find("Wall4").transform.position.z;

        float randomPositionX = Random.Range(wall1 - 0.5f, wall2 - 0.5f);
        float randomPositionZ = Random.Range(wall3 - 0.5f, wall4 - 0.5f);

        GameObject temp = Instantiate(QuestionPrefab, new Vector3(randomPositionX, 20f, randomPositionZ), Quaternion.identity);
        temp.transform.parent = ImageTarget.transform;
        temp.transform.localScale = new Vector3(0.8f, 0.01f, 0.8f);
    }
}