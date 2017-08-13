using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The different difficulty levels.
/// </summary>
public enum Difficulty {
    EASY, NORMAL, HARD
}

/// <summary>
/// 
/// </summary>
public class QuestionSingleTon : MonoBehaviour {
    
    public List<Question> m_Questions;
    public ServerResult m_Questionnaire;
    public int m_QuestionsAmount;
    public Difficulty m_Difficulty = Difficulty.NORMAL;
    public CreateObjectFromJson m_JsonQuestions;

    private static QuestionSingleTon mInstance;

    public static QuestionSingleTon Instance {
        get {
            if (mInstance == null)
                mInstance = FindObjectOfType<QuestionSingleTon>();
            
            return mInstance;
        }
    }

    void Awake() {
        //CreateObjectFromJson.LoadPlayerPrefs();
        PopulateQuestionsFromQuestionnaireJson();
        DontDestroyOnLoad(gameObject);
    }
    
    /// <summary>
    /// Populates the m_Questions list with the questions of the ServerResult object 
    /// created by the CreateQuestionnaireFromJson() method.
    /// </summary>
    public void PopulateQuestionsFromQuestionnaireJson() {
        m_Questions = new List<Question>();
        m_JsonQuestions = CreateObjectFromJson.CreateQuestionnaireFromJson();
        
        foreach (Question p in m_JsonQuestions.m_Questionnaire.result.questions) {
            p.m_RightAlternative = p.m_AnswerA;
            m_Questions.Add(p);
        }
        m_QuestionsAmount = m_Questions.Count;
    }
}