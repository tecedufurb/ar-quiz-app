using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for showing the player's score when the game is over.
/// </summary>
/// Originally attached to the ScoreBar object.
public class ScoreBar : MonoBehaviour {

	[SerializeField] private Transform ProgressScore;
	[SerializeField] private Transform TextScoreIndicator;
	[SerializeField] private Quiz Quiz;

	private float mCurrentScore;
	private float mCurrentTime;
	private float mFullScore;
	private float mSpeed = 50f;

	void Start() {
        int score = 0;
        switch (QuestionSingleTon.Instance.Difficulty) {
            case Difficulty.EASY:
                score = 10;
                break;
            case Difficulty.NORMAL:
                score = 30;
                break;
            case Difficulty.HARD:
                score = 50;
                break;
        }
        mFullScore = QuestionSingleTon.Instance.QuestionsAmount * score;
        mCurrentScore = Quiz.Score;
    }

	void FixedUpdate () {
		mCurrentTime += mSpeed * Time.deltaTime;

		if(mCurrentTime <= mCurrentScore){
			TextScoreIndicator.GetComponent<Text>().text = ((int)mCurrentTime).ToString() + "/" + mFullScore.ToString();
			ProgressScore.GetComponent<Image>().fillAmount = mCurrentTime / mFullScore;
		}
	}
}