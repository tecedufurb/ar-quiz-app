using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for make the ScoreText prefab fade away after have been instantiated.
/// </summary>
public class ScoreText : MonoBehaviour {

	private float mSpeed;
	private Vector3 mDirection;
	private float mFadeTime;
	
	void Update () {
		float translation = mSpeed * Time.deltaTime;
		transform.Translate (mDirection * translation);
	}

    /// <summary>
    /// Start the FadeOut coroutine that makes the text fade away.
    /// </summary>
    /// <param name="speed">Speed that the text will move.</param>
    /// <param name="direction">Direction that the text will go.</param>
    /// <param name="fadeTime">Time until the text fade away.</param>
	public void Inicialize(float speed, Vector3 direction, float fadeTime){
		mSpeed = speed;
		mDirection = direction;
		mFadeTime = fadeTime;

		StartCoroutine (FadeOut());
	}

    /// <summary>
    /// Changes the alpha value of the text.
    /// </summary>
    /// <returns>null</returns>
	private IEnumerator FadeOut(){
		float starAlpha = GetComponent<Text> ().color.a;

		float rate = 1.0f / mFadeTime;
		float progress = 0.0f;

		while(progress < 1.0){
			Color tempColor = GetComponent<Text> ().color;
			GetComponent<Text> ().color = new Color (tempColor.r, tempColor.g, tempColor.b, Mathf.Lerp(starAlpha, 0, progress));

			progress += rate * Time.deltaTime;
		
			yield return null;
		}

		Destroy (gameObject);
	}
}
