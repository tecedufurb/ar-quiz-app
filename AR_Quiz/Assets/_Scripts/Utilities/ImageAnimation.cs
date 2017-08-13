using UnityEngine;

/// <summary>
/// Increase and decrease the scale of an object.
/// </summary>
/// Originally attached to the TitlePanelMainMenu object.
public class ImageAnimation : MonoBehaviour {

    [SerializeField] private float MaxSize;
    [SerializeField] private float Speed;

    private bool mMaxSize;

	void Update () {
        if (!mMaxSize && (transform.localScale.x < MaxSize))
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * Speed, transform.localScale.y + Time.deltaTime * Speed, transform.localScale.z);
        else
            mMaxSize = true;

        if (mMaxSize && (transform.localScale.x > 1))
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * Speed, transform.localScale.y - Time.deltaTime * Speed, transform.localScale.z);
        else
            mMaxSize = false;
    }
}
