using UnityEngine;
/// <summary>
/// Responsible for handle the collisions between player and question.
/// </summary>
/// Originally attached to the Question prefab.
public class QuestionTriggerController : MonoBehaviour {

    public static bool m_FlagArrow = false;

    private Quiz mQuiz;
    private QuestionScreenBehavior mQuestionScreenBehavior;

    void Start() {
        mQuiz = FindObjectOfType<Quiz>();
        mQuestionScreenBehavior = FindObjectOfType<QuestionScreenBehavior>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            mQuestionScreenBehavior.EraseAnswerMessege();
            mQuestionScreenBehavior.EnableQuestionPanel(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            mQuestionScreenBehavior.EnableQuestionPanel(false);
            mQuiz.ShowQuestion();
        }
    }

    void OnDestroy() {
        m_FlagArrow = true;
    }
}
