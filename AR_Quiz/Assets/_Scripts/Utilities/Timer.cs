using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for the timer. If the time ends, the game is over.
/// </summary>
/// Needs to be attached to some UI Text component.
public class Timer : MonoBehaviour {
    
	private QuestionScreenBehavior mQuestionScreenBehavior;
	private float mStartTime;
	private float mTime;
	private string mSeconds;
	private string mMinutes;
	private int mSubtractTime;
	private float mSpeed = 0.017f;

	void Start(){
		mStartTime = 120f;
		mTime = 0f;
        mQuestionScreenBehavior = FindObjectOfType<QuestionScreenBehavior>();
	}

	void FixedUpdate(){
		mTime = mStartTime - mSubtractTime * mSpeed;

		if (mTime > 0) {
			mMinutes = ((int)mTime / 60).ToString ("00");
			mSeconds = (mTime % 60).ToString ("00");
			GetComponent<Text>().text = mMinutes + ":" + mSeconds;
		} else {
            mQuestionScreenBehavior.EnableMainPanel(false);
            mQuestionScreenBehavior.EnableQuestionPanel(false);
            mQuestionScreenBehavior.EnableFinalPanel(true);
            gameObject.SetActive(false);
		}

		mSubtractTime++;
    }
}
