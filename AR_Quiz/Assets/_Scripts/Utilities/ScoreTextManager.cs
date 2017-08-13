using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for instantiate the ScoreText prefabs.
/// </summary>
/// Attached to the _ScoreTextManager object.
public class ScoreTextManager : MonoBehaviour {
    
	[SerializeField] private GameObject ScoreText;
    [SerializeField] private RectTransform Canvas;
	[SerializeField] private float Speed;
	[SerializeField] private float FadeTime;

	private static ScoreTextManager mInstance;

	public static ScoreTextManager Instance{
		get{ 
			if(mInstance == null){
				mInstance = FindObjectOfType<ScoreTextManager>();
			}
			return mInstance;
		}
	}

    /// <summary>
    /// Instantiate the ScoreText prefab and calls it Inicialize function.
    /// </summary>
    /// <param name="position">Position where the text will be instantiated.</param>
    /// <param name="text">Text content.</param>
    /// <param name="color">Text color.</param>
	public void CreateText(Vector3 position, string text, Color color){
		Vector3 m_Direction = new Vector3(0, -1, 0);
		GameObject scoreText = Instantiate(ScoreText, position, Quaternion.identity);
		scoreText.transform.SetParent (Canvas.transform);
		scoreText.GetComponent<ScoreText> ().Inicialize (Speed, m_Direction, FadeTime);
		scoreText.GetComponent<Text> ().text = text;
		scoreText.GetComponent<Text> ().color = color;
    }
}
