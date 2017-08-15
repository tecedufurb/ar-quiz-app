using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The difficulty levels.
/// </summary>
public enum Difficulty {
    EASY, NORMAL, HARD
}

/// <summary>
/// Responsible for create a list of questions from the ResultServer object. 
/// This class persists between scenes.
/// </summary>
/// Originally attached to the _Singonton object
public class QuestionSingleTon : MonoBehaviour {
    
    private List<Question> mQuestions;
    private int mQuestionsAmount;
    private Difficulty mDifficulty = Difficulty.NORMAL;
    private CreateObjectFromJson mJsonQuestions;
    private static QuestionSingleTon mInstance;

    #region PUBLIC_PROPERTIES
    /// <summary>
    /// Property of mQuestions variable.
    /// </summary>
    public List<Question> Questions {
        get {
            return mQuestions;
        }

        set {
            mQuestions = value;
        }
    }

    /// <summary>
    /// Property of mQuestionsAmount variable.
    /// </summary>
    public int QuestionsAmount {
        get {
            return mQuestionsAmount;
        }

        set {
            mQuestionsAmount = value;
        }
    }

    /// <summary>
    /// Property of mDifficulty variable.
    /// </summary>
    public Difficulty Difficulty {
        get {
            return mDifficulty;
        }

        set {
            mDifficulty = value;
        }
    }

    /// <summary>
    /// Property of mJsonQuestions variable.
    /// </summary>
    public CreateObjectFromJson JsonQuestions {
        get {
            return mJsonQuestions;
        }

        set {
            mJsonQuestions = value;
        }
    }

    /// <summary>
    /// Property of mInstance variable.
    /// </summary>
    public static QuestionSingleTon Instance {
        get {
            if (mInstance == null)
                mInstance = FindObjectOfType<QuestionSingleTon>();

            return mInstance;
        }
    }
    #endregion

    void Awake() {
        CreateObjectFromJson.SetPlayerPrefs();
        PopulateQuestionsFromServerResult();

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Populates the Questions list with the questions of the ServerResult object.
    /// </summary>
    public void PopulateQuestionsFromServerResult() {
        Questions = new List<Question>();
        JsonQuestions = CreateObjectFromJson.CreateServerResultFromJson();
        
        foreach (Question p in JsonQuestions.m_ServerResult.result.questions) {
            p.m_RightAlternative = p.m_AnswerA;
            Questions.Add(p);
        }
        QuestionsAmount = Questions.Count;
    }
}