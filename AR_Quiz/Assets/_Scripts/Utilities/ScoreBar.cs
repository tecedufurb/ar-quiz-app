using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for showing the player's score at the end of the game.
/// </summary>
/// Attached to the ScoreBar object.
public class ScoreBar : MonoBehaviour {

	[SerializeField] private Transform LoadingBar;
	[SerializeField] private Transform TextScoreIndicator;
	[SerializeField] private Quiz Quiz;

	private float mCurrentScore;
	private float mCurrentTime;
	private float mFullScore;
	private float mSpeed = 50f;

	void Start() {
        int mScore = 0;
        switch (QuestionSingleTon.Instance.m_Difficulty) {
            case Difficulty.EASY:
                mScore = 10;
                break;
            case Difficulty.NORMAL:
                mScore = 30;
                break;
            case Difficulty.HARD:
                mScore = 50;
                break;
        }
        mFullScore = QuestionSingleTon.Instance.m_QuestionsAmount * mScore;
        mCurrentScore = Quiz.m_Score;
    }

	void FixedUpdate () {
		mCurrentTime += mSpeed * Time.deltaTime;

		if(mCurrentTime <= mCurrentScore){
			TextScoreIndicator.GetComponent<Text>().text = ((int)mCurrentTime).ToString() + "/" + mFullScore.ToString();
			LoadingBar.GetComponent<Image>().fillAmount = mCurrentTime / mFullScore;
		}
	}
}