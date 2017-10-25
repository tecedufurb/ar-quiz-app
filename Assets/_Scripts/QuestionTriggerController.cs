using UnityEngine;
/// <summary>
/// Responsible for handle the collisions between player and question.
/// </summary>
/// Originally attached to the Question prefab.
public class QuestionTriggerController : MonoBehaviour {

    public static bool m_FlagArrow = false;

    private Quiz mQuiz;
    private GameScreenHandle mQuestionScreenBehavior;

    void Start() {
        mQuiz = FindObjectOfType<Quiz>();
        mQuestionScreenBehavior = FindObjectOfType<GameScreenHandle>();

        //StartCoroutine(ResizeObject.ChangeRotation(transform.Find("question_mark").gameObject, 3, 'y'));
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            mQuestionScreenBehavior.EraseAnswerMessage();
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
